export interface IMatchReadyEvent {
  matchId: string;
  map: string;
  expiresAt: string;
  lobbies: string[];
}

export interface IMatchAcceptedEvent {
  matchId: string;
  userId: string;
  lobbies: string[];
}

export interface IMatchTimedOutEvent {
  matchId: string;
  lobbies: string[];
}

export interface IMatchStartingEvent {
  matchId: string;
  lobbies: string[];
}

export interface IMatchStartedEvent {
  matchId: string;
  address: string;
  map: string;
  mode: number;
  players: string[];
  lobbies: string[];
}

export interface IMatchCanceledEvent {
  matchId: string;
  lobbies: string[];
}

export interface IMatchFinishedEvent {
  matchId: string;
  scoreT: number;
  scoreCT: number;
  lobbies: string[];
}

export interface IInvitationAcceptedEvent {
  lobbyId: string;
  invitedUserId: string;
}

export interface IInvitationCanceledEvent {
  lobbyId: string;
  invitedUserId: string;
}

export interface IInvitationCreatedEvent {
  lobbyId: string;
  invitedUserId: string;
}

export interface ILobbyConfigurationChangedEvent {
  lobbyId: string;
  map: string;
  name: string;
  mode: number;
}

export interface ILobbyStatusChangedEvent {
  lobbyId: string;
  status: number;
}

export interface IMemberLeftEvent {
  lobbyId: string;
  userId: string;
}

export interface IMemberRoleChangedEvent {
  lobbyId: string;
  userId: string;
  role: number;
}