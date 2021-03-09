import { IUserDto } from "./IUserDto";

export interface IFriendshipDto {
  user1: IUserDto;
  user2: IUserDto;
  invitingUserId: string;
  blockingUsersIds: string[];
  isAccepted: boolean;
}