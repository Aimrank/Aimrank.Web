import { ILobbyConfiguration } from "./ILobbyConfiguration";
import { ILobbyMember } from "./ILobbyMember";
import { LobbyStatus } from "./ILobbyStatus";

export interface ILobbyDto {
  id: string;
  configuration: ILobbyConfiguration;
  status: LobbyStatus;
  members: ILobbyMember[];
}