import { HttpClient } from "@/modules/common/services/HttpClient";
import { Service } from "@/modules/common/services/Service";

interface IUserDetailsDto {
  userId: string;
  steamId: string | null;
  username: string;
}

export class UserService extends Service {
  constructor(private readonly httpClient: HttpClient) {
    super({
      getUserDetails: "/user/{userId}"
    });
  }

  public async getUserDetails(userId: string)
  {
    return this.wrap<IUserDetailsDto>(this.httpClient.get(this.getRoute("getUserDetails", { userId })));
  }
}
