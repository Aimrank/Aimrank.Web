const path = require("path");
const { VueLoaderPlugin } = require("vue-loader");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

const paths = {
  bundleEntry: path.join(__dirname, "src/main.ts"),
  bundleOutputPath: path.join(__dirname, "../wwwroot/base"),
  bundleOutputFilename: "main.js",
  cssFilename: "main.css",
  assetsImages: path.relative(__dirname, "images")
}

function createWebpackConfig(environment) {
  const env = environment.dev ? "dev" : "prod";

  return {
    mode: getMode(env),
    devtool: getDevtool(env),
    entry: paths.bundleEntry,
    output: {
      path: paths.bundleOutputPath,
      filename: paths.bundleOutputFilename,
      publicPath: '/base/'
    },
    module: {
      rules: [
        {
          test: /\.ts$/,
          loader: "ts-loader",
          options: {
            appendTsSuffixTo: [/\.vue$/]
          }
        },
        {
          test: /\.vue$/,
          loader: "vue-loader",
          options: {
            esModule: true
          }
        },
        {
          test: /\.s?css$/,
          use: [
            MiniCssExtractPlugin.loader,
            {
              loader: "css-loader",
              options: {
                modules: {
                  localIdentName: env === "prod"
                    ? "[hash:base64]"
                    : "[path][name]__[hash:base64]"
                }
              }
            },
            "sass-loader"
          ]
        },
        {
          test: /\.(png|jpe?g)$/,
          loader: "file-loader",
          options: {
            outputPath: paths.assetsImages,
            name: "[name].[hash].[ext]"
          }
        }
      ]
    },
    resolve: {
      extensions: [".ts", ".js", ".vue"],
      alias: {
        "@": path.resolve(__dirname, "src")
      }
    },
    plugins: [
      new VueLoaderPlugin(),
      new MiniCssExtractPlugin({
        filename: paths.cssFilename
      })
    ]
  };
}

function getMode(env) {
  return env === "dev" ? "development" : "production";
}

function getDevtool(env) {
  return env === "dev" ? "inline-source-map" : "";
}

module.exports = createWebpackConfig;
