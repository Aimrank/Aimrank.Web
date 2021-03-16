import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios";

interface IHttpClientConfig {
  baseUrl?: string;
  refreshTokenEnabled: boolean;
  refreshTokenEndpoint?: string;
}

export class HttpClient {
  private readonly axios: AxiosInstance = axios.create();

  public async get<T = any, R = AxiosResponse<T>>(url: string, config?: AxiosRequestConfig): Promise<R> {
    return this.axios.get<T, R>(url, config);
  }

  public async post<T = any, R = AxiosResponse<T>>(url: string, data?: any, config?: AxiosRequestConfig): Promise<R> {
    return this.axios.post<T, R>(url, data, config);
  }

  public async put<T = any, R = AxiosResponse<T>>(url: string, data?: any, config?: AxiosRequestConfig): Promise<R> {
    return this.axios.put<T, R>(url, data, config);
  }

  public async delete<T = any, R = AxiosResponse<T>>(url: string, config?: AxiosRequestConfig): Promise<R> {
    return this.axios.delete<T, R>(url, config);
  }
}
