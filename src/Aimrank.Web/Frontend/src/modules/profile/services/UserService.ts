import { HttpClient } from "@/common/services/HttpClient";
import { Service } from "@/common/services/Service";
import { IUserDto } from "@/profile/models/IUserDto";

export class UserService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      browse: "/user",
      getUserDetails: "/user/{userId}"
    });
  }

  public async browse(name: string) {
    return this.wrap<IUserDto[]>(this.httpClient.get(this.getRoute("browse"), { params: { name }}));
  }

  public async getUserDetails(userId: string) {
    return this.wrap<IUserDto>(this.httpClient.get(this.getRoute("getUserDetails", { userId })));
  }
}
