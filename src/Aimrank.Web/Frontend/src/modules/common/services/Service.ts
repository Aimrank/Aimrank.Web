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
}
