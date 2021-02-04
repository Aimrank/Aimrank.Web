import { HttpClient } from "@/modules/common/services/HttpClient";
import { Service } from "@/modules/common/services/Service";

export interface ISignUpRequest {
  email: string;
  username: string;
  password: string;
  repeatPassword: string;
}

export interface ISignInRequest {
  usernameOrEmail: string;
  password: string;
}

interface IAuthenticationSuccessResponse {
  jwt: string;
  refreshToken: string;
}

export class AuthService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      signIn: "/identity/sign-in",
      signUp: "/identity/sign-up"
    });
  }

  public async signIn(request: ISignInRequest) {
    return this.wrap<IAuthenticationSuccessResponse>(this.httpClient.post(this.getRoute("signIn"), request));
  }

  public async signUp(request: ISignUpRequest) {
    return this.wrap<IAuthenticationSuccessResponse>(this.httpClient.post(this.getRoute("signUp"), request));
  }
}
