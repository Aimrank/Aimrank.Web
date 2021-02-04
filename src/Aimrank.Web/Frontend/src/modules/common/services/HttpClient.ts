import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios";
import { parseJwt } from "@/modules/authentication/utilities/tokenParser";
import { paramsSerializer } from "@/modules/common/utilities/paramsSerializer";

interface IHttpClientConfig {
  baseUrl?: string;
  refreshTokenEnabled: boolean;
  refreshTokenEndpoint?: string;
}

export class HttpClient {
  private readonly axios: AxiosInstance = axios.create();
  private authorizationToken?: string;
  private refreshToken?: string;

  constructor(private readonly config: IHttpClientConfig) {
    this.axios.defaults.paramsSerializer = paramsSerializer;
    this.axios.defaults.baseURL = config.baseUrl;
  }

  public setAuthorizationToken(authorizationToken?: string, refreshToken?: string) {
    this.authorizationToken = authorizationToken;
    this.refreshToken = refreshToken;
    this.saveTokens();
  }

  public async get<T = any, R = AxiosResponse<T>>(url: string, config?: AxiosRequestConfig): Promise<R> {
    await this.refreshAuthorizationTokenIfExpired();
    return this.axios.get<T, R>(url, this.attachAuthorization(config));
  }

  public async post<T = any, R = AxiosResponse<T>>(url: string, data?: any, config?: AxiosRequestConfig): Promise<R> {
    await this.refreshAuthorizationTokenIfExpired();
    return this.axios.post<T, R>(url, data, this.attachAuthorization(config));
  }

  public async put<T = any, R = AxiosResponse<T>>(url: string, data?: any, config?: AxiosRequestConfig): Promise<R> {
    await this.refreshAuthorizationTokenIfExpired();
    return this.axios.put<T, R>(url, data, this.attachAuthorization(config));
  }

  public async delete<T = any, R = AxiosResponse<T>>(url: string, config?: AxiosRequestConfig): Promise<R> {
    await this.refreshAuthorizationTokenIfExpired();
    return this.axios.delete<T, R>(url, this.attachAuthorization(config));
  }

  public getUserId(): string | null {
    return this.authorizationToken
      ? parseJwt(this.authorizationToken)?.id
      : null
  }

  public getUserEmail(): string | null {
    return this.authorizationToken
      ? parseJwt(this.authorizationToken)?.email
      : null
  }

  public loadTokens() {
    const data = window.localStorage.getItem("HttpClient");
    if (data) {
      const tokens = JSON.parse(data);
      this.authorizationToken = tokens.authorizationToken;
      this.refreshToken = tokens.refreshToken;
    }
  }
  
  public async accessTokenFactory() {
    await this.refreshAuthorizationTokenIfExpired();
    return this.authorizationToken || "";
  }

  private attachAuthorization(config?: AxiosRequestConfig) {
    const newConfig: AxiosRequestConfig = config ? config : {};

    if (this.authorizationToken) {
      newConfig.headers = newConfig.headers || {};
      newConfig.headers["Authorization"] = `Bearer ${this.authorizationToken}`;
    }

    return newConfig;
  }

  private async refreshAuthorizationTokenIfExpired() {
    if (this.config.refreshTokenEndpoint && this.isTokenExpired()) {
      try {
        const res = await this.axios.post(this.config.refreshTokenEndpoint, {
          jwt: this.authorizationToken,
          refreshToken: this.refreshToken
        });

        this.setAuthorizationToken(res.data.jwt, res.data.refreshToken);
      } catch (error) {
        // Session expired - send user to sign in page
      }
    }
  }
  
  private isTokenExpired() {
    if (this.authorizationToken) {
      const parsed = parseJwt(this.authorizationToken);
      
      if (parsed.exp * 1000 < Date.now()) {
        return true;
      }
    }
    
    return false;
  }

  private saveTokens() {
    if (this.authorizationToken && this.refreshToken) {
      window.localStorage.setItem("HttpClient", JSON.stringify({
        authorizationToken: this.authorizationToken,
        refreshToken: this.refreshToken
      }));
    } else {
      window.localStorage.removeItem("HttpClient");
    }
  }
}
