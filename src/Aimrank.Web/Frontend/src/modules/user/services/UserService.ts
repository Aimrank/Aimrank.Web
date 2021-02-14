import { HttpClient } from "@/common/services/HttpClient";
import { Service } from "@/common/services/Service";

export interface IUserDetailsDto {
  userId: string;
  steamId: string | null;
  username: string;
}

export class UserService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      browse: "/user",
      getUserDetails: "/user/{userId}"
    });
  }

  public async browse(name: string) {
    return this.wrap<IUserDetailsDto[]>(this.httpClient.get(this.getRoute("browse"), { params: { name }}));
  }

  public async getUserDetails(userId: string) {
    return this.wrap<IUserDetailsDto>(this.httpClient.get(this.getRoute("getUserDetails", { userId })));
  }
}
