import { AsyncResult, Result } from "@/modules/common/models/Result";
import { ErrorResponse } from "@/modules/common/models/ErrorResponse";

interface IEndpoints {
  [name: string]: string;
}

export abstract class Service {
  protected readonly endpoints: IEndpoints;
  
  protected constructor(endpoints: IEndpoints) {
    this.endpoints = endpoints;
  }
  
  protected getRoute(name: string, params?: { [key: string]: any }) {
    let route = this.endpoints[name];
    
    if (params) {
      for (const key in params) {
        route = route.replace(new RegExp(`{${key}}`, "g"), params[key]);
      }
    }
    
    return route;
  }

  protected async wrap<TResponse>(promise): AsyncResult<TResponse, ErrorResponse> {
    try {
      const res = await promise;

      return Result.success(res.data);
    } catch (error) {
      return Result.fail(ErrorResponse.create(error.response.data));
    }
  }
}
