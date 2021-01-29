import { AsyncResult, Result } from "@/modules/common/models/Result";
import { ErrorResponse } from "@/modules/common/models/ErrorResponse";
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

  public async signIn(request: ISignInRequest): AsyncResult<IAuthenticationSuccessResponse, ErrorResponse> {
    try {
      const res = await this.httpClient.post(this.getRoute("signIn"), request);

      return Result.success(res.data);
    } catch (error) {
      return Result.fail(ErrorResponse.create(error.response.data));
    }
  }

  public async signUp(request: ISignUpRequest): AsyncResult<IAuthenticationSuccessResponse, ErrorResponse> {
    try {
      const res = await this.httpClient.post(this.getRoute("signUp"), request);

      return Result.success(res.data);
    } catch (error) {
      return Result.fail(ErrorResponse.create(error.response.data));
    }
  }
}
