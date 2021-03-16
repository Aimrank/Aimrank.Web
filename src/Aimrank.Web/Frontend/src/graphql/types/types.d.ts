export type Maybe<T> = T | null;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  Uuid: any;
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: any;
};




export type Query = {
  __typename?: 'Query';
  users?: Maybe<UserConnection>;
  matches?: Maybe<MatchConnection>;
  blockedUsers?: Maybe<UserConnection>;
  friendshipInvitations?: Maybe<UserConnection>;
  user?: Maybe<User>;
  friendship?: Maybe<Friendship>;
  lobby?: Maybe<Lobby>;
};


export type QueryUsersArgs = {
  username?: Maybe<Scalars['String']>;
  skip?: Maybe<Scalars['Int']>;
  take?: Maybe<Scalars['Int']>;
};


export type QueryMatchesArgs = {
  filter?: Maybe<FinishedMatchesFilterInput>;
  skip?: Maybe<Scalars['Int']>;
  take?: Maybe<Scalars['Int']>;
};


export type QueryBlockedUsersArgs = {
  skip?: Maybe<Scalars['Int']>;
  take?: Maybe<Scalars['Int']>;
};


export type QueryFriendshipInvitationsArgs = {
  skip?: Maybe<Scalars['Int']>;
  take?: Maybe<Scalars['Int']>;
};


export type QueryUserArgs = {
  userId: Scalars['Uuid'];
};


export type QueryFriendshipArgs = {
  userId: Scalars['Uuid'];
};

/** A connection to a list of items. */
export type UserConnection = {
  __typename?: 'UserConnection';
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** A list of edges. */
  edges?: Maybe<Array<UserEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Maybe<User>>>;
  totalCount: Scalars['Int'];
};

/** A connection to a list of items. */
export type MatchConnection = {
  __typename?: 'MatchConnection';
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** A list of edges. */
  edges?: Maybe<Array<MatchEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Maybe<Match>>>;
  totalCount: Scalars['Int'];
};

export enum ApplyPolicy {
  BeforeResolver = 'BEFORE_RESOLVER',
  AfterResolver = 'AFTER_RESOLVER'
}

export type User = {
  __typename?: 'User';
  friends?: Maybe<UserConnection>;
  stats?: Maybe<UserStatsDto>;
  id: Scalars['Uuid'];
  username?: Maybe<Scalars['String']>;
};


export type UserFriendsArgs = {
  skip?: Maybe<Scalars['Int']>;
  take?: Maybe<Scalars['Int']>;
};

/** Information about pagination in a connection. */
export type PageInfo = {
  __typename?: 'PageInfo';
  /** Indicates whether more edges exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean'];
  /** Indicates whether more edges exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean'];
  /** When paginating backwards, the cursor to continue. */
  startCursor?: Maybe<Scalars['String']>;
  /** When paginating forwards, the cursor to continue. */
  endCursor?: Maybe<Scalars['String']>;
};

/** An edge in a connection. */
export type UserEdge = {
  __typename?: 'UserEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node?: Maybe<User>;
};

export type Match = {
  __typename?: 'Match';
  id: Scalars['Uuid'];
  map?: Maybe<Scalars['String']>;
  winner: Scalars['Int'];
  scoreT: Scalars['Int'];
  scoreCT: Scalars['Int'];
  mode: Scalars['Int'];
  createdAt: Scalars['DateTime'];
  finishedAt: Scalars['DateTime'];
  teamTerrorists?: Maybe<Array<Maybe<MatchPlayer>>>;
  teamCounterTerrorists?: Maybe<Array<Maybe<MatchPlayer>>>;
};

/** An edge in a connection. */
export type MatchEdge = {
  __typename?: 'MatchEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node?: Maybe<Match>;
};

export type Mutation = {
  __typename?: 'Mutation';
  signIn?: Maybe<SignInPayload>;
  signUp?: Maybe<SignUpPayload>;
  signOut?: Maybe<SignOutPayload>;
  inviteUserToFriendsList?: Maybe<InviteUserToFriendsListPayload>;
  acceptFriendshipInvitation?: Maybe<AcceptFriendshipInvitationPayload>;
  declineFriendshipInvitation?: Maybe<DeclineFriendshipInvitationPayload>;
  blockUser?: Maybe<BlockUserPayload>;
  unblockUser?: Maybe<UnblockUserPayload>;
  deleteFriendship?: Maybe<DeleteFriendshipPayload>;
  acceptMatch?: Maybe<AcceptMatchPayload>;
  createLobby?: Maybe<CreateLobbyPayload>;
  inviteUserToLobby?: Maybe<InviteUserToLobbyPayload>;
  acceptLobbyInvitation?: Maybe<AcceptLobbyInvitationPayload>;
  cancelLobbyInvitation?: Maybe<CancelLobbyInvitationPayload>;
  changeLobbyConfiguration?: Maybe<ChangeLobbyConfigurationPayload>;
  leaveLobby?: Maybe<LeaveLobbyPayload>;
  startSearchingForGame?: Maybe<StartSearchingForGamePayload>;
  cancelSearchingForGame?: Maybe<CancelSearchingForGamePayload>;
};


export type MutationSignInArgs = {
  command?: Maybe<AuthenticateCommandInput>;
};


export type MutationSignUpArgs = {
  command?: Maybe<RegisterNewUserCommandInput>;
};


export type MutationInviteUserToFriendsListArgs = {
  command?: Maybe<InviteUserToFriendsListCommandInput>;
};


export type MutationAcceptFriendshipInvitationArgs = {
  command?: Maybe<AcceptFriendshipInvitationCommandInput>;
};


export type MutationDeclineFriendshipInvitationArgs = {
  command?: Maybe<DeclineFriendshipInvitationCommandInput>;
};


export type MutationBlockUserArgs = {
  command?: Maybe<BlockUserCommandInput>;
};


export type MutationUnblockUserArgs = {
  command?: Maybe<UnblockUserCommandInput>;
};


export type MutationDeleteFriendshipArgs = {
  command?: Maybe<DeleteFriendshipCommandInput>;
};


export type MutationAcceptMatchArgs = {
  command?: Maybe<AcceptMatchCommandInput>;
};


export type MutationCreateLobbyArgs = {
  command?: Maybe<CreateLobbyCommandInput>;
};


export type MutationInviteUserToLobbyArgs = {
  command?: Maybe<InvitePlayerToLobbyCommandInput>;
};


export type MutationAcceptLobbyInvitationArgs = {
  command?: Maybe<AcceptLobbyInvitationCommandInput>;
};


export type MutationCancelLobbyInvitationArgs = {
  command?: Maybe<CancelLobbyInvitationCommandInput>;
};


export type MutationChangeLobbyConfigurationArgs = {
  command?: Maybe<ChangeLobbyConfigurationCommandInput>;
};


export type MutationLeaveLobbyArgs = {
  command?: Maybe<LeaveLobbyCommandInput>;
};


export type MutationStartSearchingForGameArgs = {
  command?: Maybe<StartSearchingForGameCommandInput>;
};


export type MutationCancelSearchingForGameArgs = {
  command?: Maybe<CancelSearchingForGameCommandInput>;
};

export type Subscription = {
  __typename?: 'Subscription';
  lobbyInvitationCreated?: Maybe<LobbyInvitationCreatedMessage>;
  friendshipInvitationCreated?: Maybe<FriendshipInvitationCreatedMessage>;
  matchAccepted?: Maybe<MatchAcceptedEvent>;
  matchReady?: Maybe<MatchReadyMessage>;
  matchStarting?: Maybe<MatchStartingEvent>;
  matchStarted?: Maybe<MatchStartedEvent>;
  matchTimedOut?: Maybe<MatchTimedOutEvent>;
  matchCanceled?: Maybe<MatchCanceledEvent>;
  matchFinished?: Maybe<MatchFinishedEvent>;
  matchPlayerLeft?: Maybe<MatchPlayerLeftEvent>;
  lobbyInvitationAccepted?: Maybe<InvitationAcceptedMessage>;
  lobbyInvitationCanceled?: Maybe<InvitationCanceledMessage>;
  lobbyConfigurationChanged?: Maybe<LobbyConfigurationChangedMessage>;
  lobbyStatusChanged?: Maybe<LobbyStatusChangedEvent>;
  lobbyMemberLeft?: Maybe<MemberLeftEvent>;
  lobbyMemberRoleChanged?: Maybe<MemberRoleChangedEvent>;
};


export type SubscriptionLobbyInvitationCreatedArgs = {
  playerId: Scalars['Uuid'];
};


export type SubscriptionFriendshipInvitationCreatedArgs = {
  userId: Scalars['Uuid'];
};


export type SubscriptionMatchAcceptedArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionMatchReadyArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionMatchStartingArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionMatchStartedArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionMatchTimedOutArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionMatchCanceledArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionMatchFinishedArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionMatchPlayerLeftArgs = {
  playerId: Scalars['Uuid'];
};


export type SubscriptionLobbyInvitationAcceptedArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionLobbyInvitationCanceledArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionLobbyConfigurationChangedArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionLobbyStatusChangedArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionLobbyMemberLeftArgs = {
  lobbyId: Scalars['Uuid'];
};


export type SubscriptionLobbyMemberRoleChangedArgs = {
  lobbyId: Scalars['Uuid'];
};

export type UserStatsDto = {
  __typename?: 'UserStatsDto';
  matchesTotal: Scalars['Int'];
  matchesWon: Scalars['Int'];
  totalKills: Scalars['Int'];
  totalDeaths: Scalars['Int'];
  totalHs: Scalars['Int'];
  modes?: Maybe<Array<Maybe<UserStatsModeDto>>>;
};

export type Friendship = {
  __typename?: 'Friendship';
  user1?: Maybe<User>;
  user2?: Maybe<User>;
  invitingUserId?: Maybe<Scalars['Uuid']>;
  isAccepted: Scalars['Boolean'];
  blockingUsersIds?: Maybe<Array<Scalars['Uuid']>>;
};

export type Lobby = {
  __typename?: 'Lobby';
  match?: Maybe<LobbyMatch>;
  members?: Maybe<Array<Maybe<LobbyMember>>>;
  invitations?: Maybe<Array<Maybe<LobbyInvitation>>>;
  id: Scalars['Uuid'];
  status: Scalars['Int'];
  configuration?: Maybe<LobbyConfiguration>;
};

export type FinishedMatchesFilterInput = {
  playerId: Scalars['Uuid'];
  mode?: Maybe<Scalars['Int']>;
  map?: Maybe<Scalars['String']>;
};



export type MatchPlayer = {
  __typename?: 'MatchPlayer';
  user?: Maybe<User>;
  team: Scalars['Int'];
  kills: Scalars['Int'];
  assists: Scalars['Int'];
  deaths: Scalars['Int'];
  hs: Scalars['Int'];
  ratingStart: Scalars['Int'];
  ratingEnd: Scalars['Int'];
};

export type AcceptMatchPayload = {
  __typename?: 'AcceptMatchPayload';
  query?: Maybe<Query>;
};

export type DeleteFriendshipPayload = {
  __typename?: 'DeleteFriendshipPayload';
  query?: Maybe<Query>;
};

export type UnblockUserPayload = {
  __typename?: 'UnblockUserPayload';
  query?: Maybe<Query>;
};

export type BlockUserPayload = {
  __typename?: 'BlockUserPayload';
  query?: Maybe<Query>;
};

export type DeclineFriendshipInvitationPayload = {
  __typename?: 'DeclineFriendshipInvitationPayload';
  query?: Maybe<Query>;
};

export type AcceptFriendshipInvitationPayload = {
  __typename?: 'AcceptFriendshipInvitationPayload';
  query?: Maybe<Query>;
};

export type InviteUserToFriendsListPayload = {
  __typename?: 'InviteUserToFriendsListPayload';
  query?: Maybe<Query>;
};

export type SignOutPayload = {
  __typename?: 'SignOutPayload';
  query?: Maybe<Query>;
  record: Scalars['Boolean'];
};

export type SignUpPayload = {
  __typename?: 'SignUpPayload';
  query?: Maybe<Query>;
  record?: Maybe<AuthenticationSuccessRecord>;
};

export type SignInPayload = {
  __typename?: 'SignInPayload';
  query?: Maybe<Query>;
  record?: Maybe<AuthenticationSuccessRecord>;
};

export type CreateLobbyPayload = {
  __typename?: 'CreateLobbyPayload';
  query?: Maybe<Query>;
};

export type InviteUserToLobbyPayload = {
  __typename?: 'InviteUserToLobbyPayload';
  query?: Maybe<Query>;
};

export type AcceptLobbyInvitationPayload = {
  __typename?: 'AcceptLobbyInvitationPayload';
  query?: Maybe<Query>;
};

export type CancelLobbyInvitationPayload = {
  __typename?: 'CancelLobbyInvitationPayload';
  query?: Maybe<Query>;
};

export type ChangeLobbyConfigurationPayload = {
  __typename?: 'ChangeLobbyConfigurationPayload';
  query?: Maybe<Query>;
};

export type LeaveLobbyPayload = {
  __typename?: 'LeaveLobbyPayload';
  query?: Maybe<Query>;
};

export type StartSearchingForGamePayload = {
  __typename?: 'StartSearchingForGamePayload';
  query?: Maybe<Query>;
};

export type CancelSearchingForGamePayload = {
  __typename?: 'CancelSearchingForGamePayload';
  query?: Maybe<Query>;
};

export type AuthenticateCommandInput = {
  usernameOrEmail?: Maybe<Scalars['String']>;
  password?: Maybe<Scalars['String']>;
};

export type RegisterNewUserCommandInput = {
  email?: Maybe<Scalars['String']>;
  username?: Maybe<Scalars['String']>;
  password?: Maybe<Scalars['String']>;
  passwordRepeat?: Maybe<Scalars['String']>;
};

export type InviteUserToFriendsListCommandInput = {
  invitedUserId: Scalars['Uuid'];
};

export type AcceptFriendshipInvitationCommandInput = {
  invitingUserId: Scalars['Uuid'];
};

export type DeclineFriendshipInvitationCommandInput = {
  invitingUserId: Scalars['Uuid'];
};

export type BlockUserCommandInput = {
  blockedUserId: Scalars['Uuid'];
};

export type UnblockUserCommandInput = {
  blockedUserId: Scalars['Uuid'];
};

export type DeleteFriendshipCommandInput = {
  userId: Scalars['Uuid'];
};

export type AcceptMatchCommandInput = {
  matchId: Scalars['Uuid'];
};

export type CreateLobbyCommandInput = {
  lobbyId: Scalars['Uuid'];
};

export type InvitePlayerToLobbyCommandInput = {
  lobbyId: Scalars['Uuid'];
  invitedPlayerId: Scalars['Uuid'];
};

export type AcceptLobbyInvitationCommandInput = {
  lobbyId: Scalars['Uuid'];
};

export type CancelLobbyInvitationCommandInput = {
  lobbyId: Scalars['Uuid'];
};

export type ChangeLobbyConfigurationCommandInput = {
  lobbyId: Scalars['Uuid'];
  map?: Maybe<Scalars['String']>;
  name?: Maybe<Scalars['String']>;
  mode: Scalars['Int'];
};

export type LeaveLobbyCommandInput = {
  lobbyId: Scalars['Uuid'];
};

export type StartSearchingForGameCommandInput = {
  lobbyId: Scalars['Uuid'];
};

export type CancelSearchingForGameCommandInput = {
  lobbyId: Scalars['Uuid'];
};

export type LobbyInvitationCreatedMessage = {
  __typename?: 'LobbyInvitationCreatedMessage';
  lobbyId: Scalars['Uuid'];
  invitingPlayerId: Scalars['Uuid'];
  invitedPlayerId: Scalars['Uuid'];
};

export type FriendshipInvitationCreatedMessage = {
  __typename?: 'FriendshipInvitationCreatedMessage';
  invitingUserId: Scalars['Uuid'];
  invitedUserId: Scalars['Uuid'];
};

export type MatchAcceptedEvent = {
  __typename?: 'MatchAcceptedEvent';
  matchId: Scalars['Uuid'];
  userId: Scalars['Uuid'];
  lobbies?: Maybe<Array<Scalars['Uuid']>>;
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type MatchReadyMessage = {
  __typename?: 'MatchReadyMessage';
  matchId: Scalars['Uuid'];
  map?: Maybe<Scalars['String']>;
  expiresAt: Scalars['DateTime'];
  lobbies?: Maybe<Array<Scalars['Uuid']>>;
};

export type MatchStartingEvent = {
  __typename?: 'MatchStartingEvent';
  matchId: Scalars['Uuid'];
  lobbies?: Maybe<Array<Scalars['Uuid']>>;
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type MatchStartedEvent = {
  __typename?: 'MatchStartedEvent';
  matchId: Scalars['Uuid'];
  map?: Maybe<Scalars['String']>;
  address?: Maybe<Scalars['String']>;
  mode: Scalars['Int'];
  players?: Maybe<Array<Scalars['Uuid']>>;
  lobbies?: Maybe<Array<Scalars['Uuid']>>;
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type MatchTimedOutEvent = {
  __typename?: 'MatchTimedOutEvent';
  matchId: Scalars['Uuid'];
  lobbies?: Maybe<Array<Scalars['Uuid']>>;
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type MatchCanceledEvent = {
  __typename?: 'MatchCanceledEvent';
  matchId: Scalars['Uuid'];
  lobbies?: Maybe<Array<Scalars['Uuid']>>;
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type MatchFinishedEvent = {
  __typename?: 'MatchFinishedEvent';
  matchId: Scalars['Uuid'];
  scoreT: Scalars['Int'];
  scoreCT: Scalars['Int'];
  lobbies?: Maybe<Array<Scalars['Uuid']>>;
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type MatchPlayerLeftEvent = {
  __typename?: 'MatchPlayerLeftEvent';
  userId: Scalars['Uuid'];
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type InvitationAcceptedMessage = {
  __typename?: 'InvitationAcceptedMessage';
  lobbyId: Scalars['Uuid'];
  invitedPlayerId: Scalars['Uuid'];
};

export type InvitationCanceledMessage = {
  __typename?: 'InvitationCanceledMessage';
  lobbyId: Scalars['Uuid'];
  invitedPlayerId: Scalars['Uuid'];
};

export type LobbyConfigurationChangedMessage = {
  __typename?: 'LobbyConfigurationChangedMessage';
  lobbyId: Scalars['Uuid'];
  map?: Maybe<Scalars['String']>;
  name?: Maybe<Scalars['String']>;
  mode: Scalars['Int'];
};

export type LobbyStatusChangedEvent = {
  __typename?: 'LobbyStatusChangedEvent';
  lobbyId: Scalars['Uuid'];
  status: Scalars['Int'];
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type MemberLeftEvent = {
  __typename?: 'MemberLeftEvent';
  lobbyId: Scalars['Uuid'];
  userId: Scalars['Uuid'];
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type MemberRoleChangedEvent = {
  __typename?: 'MemberRoleChangedEvent';
  lobbyId: Scalars['Uuid'];
  userId: Scalars['Uuid'];
  role: Scalars['Int'];
  id: Scalars['Uuid'];
  occurredAt: Scalars['DateTime'];
};

export type UserStatsModeDto = {
  __typename?: 'UserStatsModeDto';
  mode: Scalars['Int'];
  matchesTotal: Scalars['Int'];
  matchesWon: Scalars['Int'];
  totalKills: Scalars['Int'];
  totalDeaths: Scalars['Int'];
  totalHs: Scalars['Int'];
  maps?: Maybe<Array<Maybe<UserStatsMapDto>>>;
};

export type LobbyMatch = {
  __typename?: 'LobbyMatch';
  id: Scalars['Uuid'];
  address?: Maybe<Scalars['String']>;
  map?: Maybe<Scalars['String']>;
  mode: Scalars['Int'];
  status: Scalars['Int'];
};

export type LobbyMember = {
  __typename?: 'LobbyMember';
  user?: Maybe<User>;
  isLeader: Scalars['Boolean'];
};

export type LobbyInvitation = {
  __typename?: 'LobbyInvitation';
  invitingUser?: Maybe<User>;
  invitedUser?: Maybe<User>;
  createdAt: Scalars['DateTime'];
};

export type LobbyConfiguration = {
  __typename?: 'LobbyConfiguration';
  map?: Maybe<Scalars['String']>;
  name?: Maybe<Scalars['String']>;
  mode: Scalars['Int'];
};

export type UserStatsMapDto = {
  __typename?: 'UserStatsMapDto';
  map?: Maybe<Scalars['String']>;
  matchesTotal: Scalars['Int'];
  matchesWon: Scalars['Int'];
  totalKills: Scalars['Int'];
  totalDeaths: Scalars['Int'];
  totalHs: Scalars['Int'];
};

export type AuthenticationSuccessRecord = {
  __typename?: 'AuthenticationSuccessRecord';
  id: Scalars['Uuid'];
  username: Scalars['String'];
  email: Scalars['String'];
};

export type SignInMutationVariables = Exact<{
  command: AuthenticateCommandInput;
}>;


export type SignInMutation = (
  { __typename?: 'Mutation' }
  & { signIn?: Maybe<(
    { __typename?: 'SignInPayload' }
    & { record?: Maybe<(
      { __typename?: 'AuthenticationSuccessRecord' }
      & Pick<AuthenticationSuccessRecord, 'id' | 'email' | 'username'>
    )> }
  )> }
);

export type SignOutMutationVariables = Exact<{ [key: string]: never; }>;


export type SignOutMutation = (
  { __typename?: 'Mutation' }
  & { signOut?: Maybe<(
    { __typename?: 'SignOutPayload' }
    & Pick<SignOutPayload, 'record'>
  )> }
);

export type SignUpMutationVariables = Exact<{
  command: RegisterNewUserCommandInput;
}>;


export type SignUpMutation = (
  { __typename?: 'Mutation' }
  & { signUp?: Maybe<(
    { __typename?: 'SignUpPayload' }
    & { record?: Maybe<(
      { __typename?: 'AuthenticationSuccessRecord' }
      & Pick<AuthenticationSuccessRecord, 'id' | 'email' | 'username'>
    )> }
  )> }
);


declare module '*/signIn.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const signIn: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/signOut.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const signOut: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/signUp.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const signUp: DocumentNode;

  export default defaultDocument;
}
    