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
  lobbyInvitations?: Maybe<Array<LobbyInvitation>>;
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

export type Lobby = {
  __typename?: 'Lobby';
  configuration: LobbyConfiguration;
  members?: Maybe<Array<LobbyMember>>;
  match?: Maybe<LobbyMatch>;
  id: Scalars['Uuid'];
  status: Scalars['Int'];
};

export type LobbyConfiguration = {
  __typename?: 'LobbyConfiguration';
  map: Scalars['String'];
  name: Scalars['String'];
  mode: Scalars['Int'];
};

export type LobbyInvitation = {
  __typename?: 'LobbyInvitation';
  invitingUser?: Maybe<User>;
  invitedUser?: Maybe<User>;
  lobbyId: Scalars['Uuid'];
  createdAt: Scalars['DateTime'];
};

export type LobbyMatch = {
  __typename?: 'LobbyMatch';
  address: Scalars['String'];
  map: Scalars['String'];
  id: Scalars['Uuid'];
  mode: Scalars['Int'];
  status: Scalars['Int'];
};

export type LobbyMember = {
  __typename?: 'LobbyMember';
  user: User;
  isLeader: Scalars['Boolean'];
};

export type Match = {
  __typename?: 'Match';
  map: Scalars['String'];
  teamTerrorists?: Maybe<Array<MatchPlayer>>;
  teamCounterTerrorists?: Maybe<Array<MatchPlayer>>;
  id: Scalars['Uuid'];
  winner: Scalars['Int'];
  scoreT: Scalars['Int'];
  scoreCT: Scalars['Int'];
  mode: Scalars['Int'];
  createdAt: Scalars['DateTime'];
  finishedAt: Scalars['DateTime'];
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

export type User = {
  __typename?: 'User';
  friends?: Maybe<UserConnection>;
  username: Scalars['String'];
  stats?: Maybe<PlayerStatsDto>;
  steamId?: Maybe<Scalars['String']>;
  id: Scalars['Uuid'];
};


export type UserFriendsArgs = {
  skip?: Maybe<Scalars['Int']>;
  take?: Maybe<Scalars['Int']>;
};

/** A connection to a list of items. */
export type UserConnection = {
  __typename?: 'UserConnection';
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** A list of edges. */
  edges?: Maybe<Array<UserEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<User>>;
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
  nodes?: Maybe<Array<Match>>;
  totalCount: Scalars['Int'];
};

export enum ApplyPolicy {
  BeforeResolver = 'BEFORE_RESOLVER',
  AfterResolver = 'AFTER_RESOLVER'
}

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
  node: User;
};

/** An edge in a connection. */
export type MatchEdge = {
  __typename?: 'MatchEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Match;
};

export type Mutation = {
  __typename?: 'Mutation';
  signIn?: Maybe<SignInPayload>;
  signUp?: Maybe<SignUpPayload>;
  signOut?: Maybe<SignOutPayload>;
  createLobby?: Maybe<CreateLobbyPayload>;
  inviteUserToLobby?: Maybe<InviteUserToLobbyPayload>;
  acceptLobbyInvitation?: Maybe<AcceptLobbyInvitationPayload>;
  cancelLobbyInvitation?: Maybe<CancelLobbyInvitationPayload>;
  changeLobbyConfiguration?: Maybe<ChangeLobbyConfigurationPayload>;
  leaveLobby?: Maybe<LeaveLobbyPayload>;
  startSearchingForGame?: Maybe<StartSearchingForGamePayload>;
  cancelSearchingForGame?: Maybe<CancelSearchingForGamePayload>;
  acceptMatch?: Maybe<AcceptMatchPayload>;
  inviteUserToFriendsList?: Maybe<InviteUserToFriendsListPayload>;
  acceptFriendshipInvitation?: Maybe<AcceptFriendshipInvitationPayload>;
  declineFriendshipInvitation?: Maybe<DeclineFriendshipInvitationPayload>;
  blockUser?: Maybe<BlockUserPayload>;
  unblockUser?: Maybe<UnblockUserPayload>;
  deleteFriendship?: Maybe<DeleteFriendshipPayload>;
};


export type MutationSignInArgs = {
  command?: Maybe<AuthenticateCommandInput>;
};


export type MutationSignUpArgs = {
  command?: Maybe<RegisterNewUserCommandInput>;
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


export type MutationAcceptMatchArgs = {
  command?: Maybe<AcceptMatchCommandInput>;
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

export type Subscription = {
  __typename?: 'Subscription';
  lobbyInvitationCreated?: Maybe<LobbyInvitationCreatedPayload>;
  friendshipInvitationCreated?: Maybe<FriendshipInvitationCreatedPayload>;
  matchAccepted?: Maybe<MatchAcceptedPayload>;
  matchReady?: Maybe<MatchReadyPayload>;
  matchStarting?: Maybe<MatchStartingPayload>;
  matchStarted?: Maybe<MatchStartedPayload>;
  matchTimedOut?: Maybe<MatchTimedOutRecord>;
  matchCanceled?: Maybe<MatchCanceledPayload>;
  matchFinished?: Maybe<MatchFinishedPayload>;
  matchPlayerLeft?: Maybe<MatchPlayerLeftPayload>;
  lobbyInvitationAccepted?: Maybe<LobbyInvitationAcceptedPayload>;
  lobbyInvitationCanceled?: Maybe<LobbyInvitationCanceledPayload>;
  lobbyConfigurationChanged?: Maybe<LobbyConfigurationChangedPayload>;
  lobbyStatusChanged?: Maybe<LobbyStatusChangedPayload>;
  lobbyMemberLeft?: Maybe<LobbyMemberLeftPayload>;
  lobbyMemberRoleChanged?: Maybe<LobbyMemberRoleChangedPayload>;
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
  lobbyId: Scalars['Uuid'];
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

export type Friendship = {
  __typename?: 'Friendship';
  user1?: Maybe<User>;
  user2?: Maybe<User>;
  invitingUserId?: Maybe<Scalars['Uuid']>;
  isAccepted: Scalars['Boolean'];
  blockingUsersIds?: Maybe<Array<Scalars['Uuid']>>;
};

export type FinishedMatchesFilterInput = {
  playerId: Scalars['Uuid'];
  mode?: Maybe<Scalars['Int']>;
  map?: Maybe<Scalars['String']>;
};



export type PlayerStatsDto = {
  __typename?: 'PlayerStatsDto';
  matchesTotal: Scalars['Int'];
  matchesWon: Scalars['Int'];
  totalKills: Scalars['Int'];
  totalDeaths: Scalars['Int'];
  totalHs: Scalars['Int'];
  modes?: Maybe<Array<Maybe<PlayerStatsModeDto>>>;
};

export type CancelSearchingForGamePayload = {
  __typename?: 'CancelSearchingForGamePayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type StartSearchingForGamePayload = {
  __typename?: 'StartSearchingForGamePayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type LeaveLobbyPayload = {
  __typename?: 'LeaveLobbyPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type ChangeLobbyConfigurationPayload = {
  __typename?: 'ChangeLobbyConfigurationPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type CancelLobbyInvitationPayload = {
  __typename?: 'CancelLobbyInvitationPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type AcceptLobbyInvitationPayload = {
  __typename?: 'AcceptLobbyInvitationPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type InviteUserToLobbyPayload = {
  __typename?: 'InviteUserToLobbyPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type CreateLobbyPayload = {
  __typename?: 'CreateLobbyPayload';
  record: Lobby;
  query?: Maybe<Query>;
  recordId: Scalars['Uuid'];
  status?: Maybe<Scalars['String']>;
};

export type RegisterNewUserCommandInput = {
  email?: Maybe<Scalars['String']>;
  username?: Maybe<Scalars['String']>;
  password?: Maybe<Scalars['String']>;
  passwordRepeat?: Maybe<Scalars['String']>;
};

export type AuthenticateCommandInput = {
  usernameOrEmail?: Maybe<Scalars['String']>;
  password?: Maybe<Scalars['String']>;
};

export type SignOutPayload = {
  __typename?: 'SignOutPayload';
  query?: Maybe<Query>;
  record: Scalars['Boolean'];
  status?: Maybe<Scalars['String']>;
};

export type SignUpPayload = {
  __typename?: 'SignUpPayload';
  query?: Maybe<Query>;
  record?: Maybe<AuthenticationSuccessRecord>;
  status?: Maybe<Scalars['String']>;
};

export type SignInPayload = {
  __typename?: 'SignInPayload';
  query?: Maybe<Query>;
  record?: Maybe<AuthenticationSuccessRecord>;
  status?: Maybe<Scalars['String']>;
};

export type AcceptMatchPayload = {
  __typename?: 'AcceptMatchPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
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

export type AcceptMatchCommandInput = {
  matchId: Scalars['Uuid'];
};

export type InviteUserToFriendsListPayload = {
  __typename?: 'InviteUserToFriendsListPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type AcceptFriendshipInvitationPayload = {
  __typename?: 'AcceptFriendshipInvitationPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type DeclineFriendshipInvitationPayload = {
  __typename?: 'DeclineFriendshipInvitationPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type BlockUserPayload = {
  __typename?: 'BlockUserPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type UnblockUserPayload = {
  __typename?: 'UnblockUserPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
};

export type DeleteFriendshipPayload = {
  __typename?: 'DeleteFriendshipPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
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

export type LobbyInvitationCreatedPayload = {
  __typename?: 'LobbyInvitationCreatedPayload';
  query?: Maybe<Query>;
  record: LobbyInvitationCreatedRecord;
};

export type FriendshipInvitationCreatedPayload = {
  __typename?: 'FriendshipInvitationCreatedPayload';
  query?: Maybe<Query>;
  record: FriendshipInvitationCreatedRecord;
};

export type MatchAcceptedPayload = {
  __typename?: 'MatchAcceptedPayload';
  query?: Maybe<Query>;
  record: MatchAcceptedRecord;
};

export type MatchReadyPayload = {
  __typename?: 'MatchReadyPayload';
  query?: Maybe<Query>;
  record: MatchReadyRecord;
};

export type MatchStartingPayload = {
  __typename?: 'MatchStartingPayload';
  query?: Maybe<Query>;
  record: MatchStartingRecord;
};

export type MatchStartedPayload = {
  __typename?: 'MatchStartedPayload';
  query?: Maybe<Query>;
  record: MatchStartedRecord;
};

export type MatchTimedOutRecord = {
  __typename?: 'MatchTimedOutRecord';
  matchId: Scalars['Uuid'];
};

export type MatchCanceledPayload = {
  __typename?: 'MatchCanceledPayload';
  query?: Maybe<Query>;
  record: MatchCanceledRecord;
};

export type MatchFinishedPayload = {
  __typename?: 'MatchFinishedPayload';
  query?: Maybe<Query>;
  record: MatchFinishedRecord;
};

export type MatchPlayerLeftPayload = {
  __typename?: 'MatchPlayerLeftPayload';
  query?: Maybe<Query>;
  record: MatchPlayerLeftRecord;
};

export type LobbyInvitationAcceptedPayload = {
  __typename?: 'LobbyInvitationAcceptedPayload';
  query?: Maybe<Query>;
  record: LobbyInvitationAcceptedRecord;
};

export type LobbyInvitationCanceledPayload = {
  __typename?: 'LobbyInvitationCanceledPayload';
  query?: Maybe<Query>;
  record: LobbyInvitationCanceledRecord;
};

export type LobbyConfigurationChangedPayload = {
  __typename?: 'LobbyConfigurationChangedPayload';
  query?: Maybe<Query>;
  record: LobbyConfigurationChangedRecord;
};

export type LobbyStatusChangedPayload = {
  __typename?: 'LobbyStatusChangedPayload';
  query?: Maybe<Query>;
  record: LobbyStatusChangedRecord;
};

export type LobbyMemberLeftPayload = {
  __typename?: 'LobbyMemberLeftPayload';
  query?: Maybe<Query>;
  record: LobbyMemberLeftRecord;
};

export type LobbyMemberRoleChangedPayload = {
  __typename?: 'LobbyMemberRoleChangedPayload';
  query?: Maybe<Query>;
  record: LobbyMemberRoleChangedRecord;
};

export type PlayerStatsModeDto = {
  __typename?: 'PlayerStatsModeDto';
  mode: Scalars['Int'];
  matchesTotal: Scalars['Int'];
  matchesWon: Scalars['Int'];
  totalKills: Scalars['Int'];
  totalDeaths: Scalars['Int'];
  totalHs: Scalars['Int'];
  maps?: Maybe<Array<Maybe<PlayerStatsMapDto>>>;
};

export type PlayerStatsMapDto = {
  __typename?: 'PlayerStatsMapDto';
  map?: Maybe<Scalars['String']>;
  matchesTotal: Scalars['Int'];
  matchesWon: Scalars['Int'];
  totalKills: Scalars['Int'];
  totalDeaths: Scalars['Int'];
  totalHs: Scalars['Int'];
};

export type LobbyMemberRoleChangedRecord = {
  __typename?: 'LobbyMemberRoleChangedRecord';
  playerId: Scalars['Uuid'];
  role: Scalars['Int'];
};

export type LobbyMemberLeftRecord = {
  __typename?: 'LobbyMemberLeftRecord';
  lobbyId: Scalars['Uuid'];
  playerId: Scalars['Uuid'];
};

export type LobbyStatusChangedRecord = {
  __typename?: 'LobbyStatusChangedRecord';
  lobbyId: Scalars['Uuid'];
  status: Scalars['Int'];
};

export type LobbyConfigurationChangedRecord = {
  __typename?: 'LobbyConfigurationChangedRecord';
  lobbyId: Scalars['Uuid'];
  map: Scalars['String'];
  name: Scalars['String'];
  mode: Scalars['Int'];
};

export type LobbyInvitationCanceledRecord = {
  __typename?: 'LobbyInvitationCanceledRecord';
  lobbyId: Scalars['Uuid'];
  invitedPlayerId: Scalars['Uuid'];
};

export type LobbyInvitationAcceptedRecord = {
  __typename?: 'LobbyInvitationAcceptedRecord';
  invitingUser?: Maybe<User>;
  lobbyId: Scalars['Uuid'];
};

export type MatchPlayerLeftRecord = {
  __typename?: 'MatchPlayerLeftRecord';
  playerId: Scalars['Uuid'];
};

export type MatchFinishedRecord = {
  __typename?: 'MatchFinishedRecord';
  matchId: Scalars['Uuid'];
  scoreT: Scalars['Int'];
  scoreCT: Scalars['Int'];
};

export type MatchCanceledRecord = {
  __typename?: 'MatchCanceledRecord';
  matchId: Scalars['Uuid'];
};

export type MatchStartedRecord = {
  __typename?: 'MatchStartedRecord';
  matchId: Scalars['Uuid'];
  map: Scalars['String'];
  address: Scalars['String'];
  mode: Scalars['Int'];
  players?: Maybe<Array<Scalars['Uuid']>>;
};

export type MatchStartingRecord = {
  __typename?: 'MatchStartingRecord';
  matchId: Scalars['Uuid'];
};

export type MatchReadyRecord = {
  __typename?: 'MatchReadyRecord';
  matchId: Scalars['Uuid'];
  map: Scalars['String'];
  expiresAt: Scalars['DateTime'];
};

export type MatchAcceptedRecord = {
  __typename?: 'MatchAcceptedRecord';
  matchId: Scalars['Uuid'];
  playerId: Scalars['Uuid'];
};

export type FriendshipInvitationCreatedRecord = {
  __typename?: 'FriendshipInvitationCreatedRecord';
  invitingUser?: Maybe<User>;
};

export type LobbyInvitationCreatedRecord = {
  __typename?: 'LobbyInvitationCreatedRecord';
  invitingUser?: Maybe<User>;
  lobbyId: Scalars['Uuid'];
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

export type GetUsersQueryVariables = Exact<{
  username: Scalars['String'];
}>;


export type GetUsersQuery = (
  { __typename?: 'Query' }
  & { users?: Maybe<(
    { __typename?: 'UserConnection' }
    & { nodes?: Maybe<Array<(
      { __typename?: 'User' }
      & Pick<User, 'id' | 'username'>
    )>> }
  )> }
);

export type LobbyFieldsFragment = (
  { __typename?: 'Lobby' }
  & Pick<Lobby, 'id' | 'status'>
  & { match?: Maybe<(
    { __typename?: 'LobbyMatch' }
    & Pick<LobbyMatch, 'id' | 'map' | 'mode' | 'status' | 'address'>
  )>, configuration: (
    { __typename?: 'LobbyConfiguration' }
    & Pick<LobbyConfiguration, 'map' | 'mode' | 'name'>
  ), members?: Maybe<Array<(
    { __typename?: 'LobbyMember' }
    & Pick<LobbyMember, 'isLeader'>
    & { user: (
      { __typename?: 'User' }
      & Pick<User, 'id' | 'username'>
    ) }
  )>> }
);

export type AcceptLobbyInvitationMutationVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type AcceptLobbyInvitationMutation = (
  { __typename?: 'Mutation' }
  & { acceptLobbyInvitation?: Maybe<(
    { __typename?: 'AcceptLobbyInvitationPayload' }
    & Pick<AcceptLobbyInvitationPayload, 'status'>
  )> }
);

export type AcceptMatchMutationVariables = Exact<{
  matchId: Scalars['Uuid'];
}>;


export type AcceptMatchMutation = (
  { __typename?: 'Mutation' }
  & { acceptMatch?: Maybe<(
    { __typename?: 'AcceptMatchPayload' }
    & Pick<AcceptMatchPayload, 'status'>
  )> }
);

export type CancelLobbyInvitationMutationVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type CancelLobbyInvitationMutation = (
  { __typename?: 'Mutation' }
  & { cancelLobbyInvitation?: Maybe<(
    { __typename?: 'CancelLobbyInvitationPayload' }
    & Pick<CancelLobbyInvitationPayload, 'status'>
  )> }
);

export type ChangeLobbyConfigurationMutationVariables = Exact<{
  input: ChangeLobbyConfigurationCommandInput;
}>;


export type ChangeLobbyConfigurationMutation = (
  { __typename?: 'Mutation' }
  & { changeLobbyConfiguration?: Maybe<(
    { __typename?: 'ChangeLobbyConfigurationPayload' }
    & Pick<ChangeLobbyConfigurationPayload, 'status'>
  )> }
);

export type CreateLobbyMutationVariables = Exact<{ [key: string]: never; }>;


export type CreateLobbyMutation = (
  { __typename?: 'Mutation' }
  & { createLobby?: Maybe<(
    { __typename?: 'CreateLobbyPayload' }
    & { record: (
      { __typename?: 'Lobby' }
      & LobbyFieldsFragment
    ) }
  )> }
);

export type InviteUserToLobbyMutationVariables = Exact<{
  lobbyId: Scalars['Uuid'];
  userId: Scalars['Uuid'];
}>;


export type InviteUserToLobbyMutation = (
  { __typename?: 'Mutation' }
  & { inviteUserToLobby?: Maybe<(
    { __typename?: 'InviteUserToLobbyPayload' }
    & Pick<InviteUserToLobbyPayload, 'status'>
  )> }
);

export type LeaveLobbyMutationVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type LeaveLobbyMutation = (
  { __typename?: 'Mutation' }
  & { leaveLobby?: Maybe<(
    { __typename?: 'LeaveLobbyPayload' }
    & Pick<LeaveLobbyPayload, 'status'>
  )> }
);

export type StartSearchingForGameMutationVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type StartSearchingForGameMutation = (
  { __typename?: 'Mutation' }
  & { startSearchingForGame?: Maybe<(
    { __typename?: 'StartSearchingForGamePayload' }
    & Pick<StartSearchingForGamePayload, 'status'>
  )> }
);

export type GetFriendsQueryVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type GetFriendsQuery = (
  { __typename?: 'Query' }
  & { user?: Maybe<(
    { __typename?: 'User' }
    & { friends?: Maybe<(
      { __typename?: 'UserConnection' }
      & { nodes?: Maybe<Array<(
        { __typename?: 'User' }
        & Pick<User, 'id' | 'username'>
      )>> }
    )> }
  )> }
);

export type GetLobbyQueryVariables = Exact<{ [key: string]: never; }>;


export type GetLobbyQuery = (
  { __typename?: 'Query' }
  & { lobby?: Maybe<(
    { __typename?: 'Lobby' }
    & LobbyFieldsFragment
  )> }
);

export type GetLobbyInvitationsQueryVariables = Exact<{ [key: string]: never; }>;


export type GetLobbyInvitationsQuery = (
  { __typename?: 'Query' }
  & { lobbyInvitations?: Maybe<Array<(
    { __typename?: 'LobbyInvitation' }
    & Pick<LobbyInvitation, 'lobbyId' | 'createdAt'>
    & { invitingUser?: Maybe<(
      { __typename?: 'User' }
      & Pick<User, 'id' | 'username'>
    )> }
  )>> }
);

export type LobbyConfigurationChangedSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type LobbyConfigurationChangedSubscription = (
  { __typename?: 'Subscription' }
  & { lobbyConfigurationChanged?: Maybe<(
    { __typename?: 'LobbyConfigurationChangedPayload' }
    & { record: (
      { __typename?: 'LobbyConfigurationChangedRecord' }
      & Pick<LobbyConfigurationChangedRecord, 'lobbyId' | 'map' | 'mode' | 'name'>
    ) }
  )> }
);

export type LobbyInvitationAcceptedSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type LobbyInvitationAcceptedSubscription = (
  { __typename?: 'Subscription' }
  & { lobbyInvitationAccepted?: Maybe<(
    { __typename?: 'LobbyInvitationAcceptedPayload' }
    & { record: (
      { __typename?: 'LobbyInvitationAcceptedRecord' }
      & Pick<LobbyInvitationAcceptedRecord, 'lobbyId'>
      & { invitingUser?: Maybe<(
        { __typename?: 'User' }
        & Pick<User, 'id' | 'username'>
      )> }
    ) }
  )> }
);

export type LobbyInvitationCreatedSubscriptionVariables = Exact<{ [key: string]: never; }>;


export type LobbyInvitationCreatedSubscription = (
  { __typename?: 'Subscription' }
  & { lobbyInvitationCreated?: Maybe<(
    { __typename?: 'LobbyInvitationCreatedPayload' }
    & { record: (
      { __typename?: 'LobbyInvitationCreatedRecord' }
      & Pick<LobbyInvitationCreatedRecord, 'lobbyId'>
      & { invitingUser?: Maybe<(
        { __typename?: 'User' }
        & Pick<User, 'id' | 'username'>
      )> }
    ) }
  )> }
);

export type LobbyMemberLeftSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type LobbyMemberLeftSubscription = (
  { __typename?: 'Subscription' }
  & { lobbyMemberLeft?: Maybe<(
    { __typename?: 'LobbyMemberLeftPayload' }
    & { record: (
      { __typename?: 'LobbyMemberLeftRecord' }
      & Pick<LobbyMemberLeftRecord, 'lobbyId' | 'playerId'>
    ) }
  )> }
);

export type LobbyMemberRoleChangedSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type LobbyMemberRoleChangedSubscription = (
  { __typename?: 'Subscription' }
  & { lobbyMemberRoleChanged?: Maybe<(
    { __typename?: 'LobbyMemberRoleChangedPayload' }
    & { record: (
      { __typename?: 'LobbyMemberRoleChangedRecord' }
      & Pick<LobbyMemberRoleChangedRecord, 'playerId' | 'role'>
    ) }
  )> }
);

export type LobbyStatusChangedSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type LobbyStatusChangedSubscription = (
  { __typename?: 'Subscription' }
  & { lobbyStatusChanged?: Maybe<(
    { __typename?: 'LobbyStatusChangedPayload' }
    & { record: (
      { __typename?: 'LobbyStatusChangedRecord' }
      & Pick<LobbyStatusChangedRecord, 'lobbyId' | 'status'>
    ) }
  )> }
);

export type MatchAcceptedSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type MatchAcceptedSubscription = (
  { __typename?: 'Subscription' }
  & { matchAccepted?: Maybe<(
    { __typename?: 'MatchAcceptedPayload' }
    & { record: (
      { __typename?: 'MatchAcceptedRecord' }
      & Pick<MatchAcceptedRecord, 'matchId' | 'playerId'>
    ) }
  )> }
);

export type MatchCanceledSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type MatchCanceledSubscription = (
  { __typename?: 'Subscription' }
  & { matchCanceled?: Maybe<(
    { __typename?: 'MatchCanceledPayload' }
    & { record: (
      { __typename?: 'MatchCanceledRecord' }
      & Pick<MatchCanceledRecord, 'matchId'>
    ) }
  )> }
);

export type MatchFinishedSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type MatchFinishedSubscription = (
  { __typename?: 'Subscription' }
  & { matchFinished?: Maybe<(
    { __typename?: 'MatchFinishedPayload' }
    & { record: (
      { __typename?: 'MatchFinishedRecord' }
      & Pick<MatchFinishedRecord, 'matchId'>
    ) }
  )> }
);

export type MatchPlayerLeftSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type MatchPlayerLeftSubscription = (
  { __typename?: 'Subscription' }
  & { matchPlayerLeft?: Maybe<(
    { __typename?: 'MatchPlayerLeftPayload' }
    & { record: (
      { __typename?: 'MatchPlayerLeftRecord' }
      & Pick<MatchPlayerLeftRecord, 'playerId'>
    ) }
  )> }
);

export type MatchReadySubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type MatchReadySubscription = (
  { __typename?: 'Subscription' }
  & { matchReady?: Maybe<(
    { __typename?: 'MatchReadyPayload' }
    & { record: (
      { __typename?: 'MatchReadyRecord' }
      & Pick<MatchReadyRecord, 'map' | 'matchId' | 'expiresAt'>
    ) }
  )> }
);

export type MatchStartedSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type MatchStartedSubscription = (
  { __typename?: 'Subscription' }
  & { matchStarted?: Maybe<(
    { __typename?: 'MatchStartedPayload' }
    & { record: (
      { __typename?: 'MatchStartedRecord' }
      & Pick<MatchStartedRecord, 'matchId' | 'address' | 'map' | 'mode'>
    ) }
  )> }
);

export type MatchStartingSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type MatchStartingSubscription = (
  { __typename?: 'Subscription' }
  & { matchStarting?: Maybe<(
    { __typename?: 'MatchStartingPayload' }
    & { record: (
      { __typename?: 'MatchStartingRecord' }
      & Pick<MatchStartingRecord, 'matchId'>
    ) }
  )> }
);

export type MatchTimedOutSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type MatchTimedOutSubscription = (
  { __typename?: 'Subscription' }
  & { matchTimedOut?: Maybe<(
    { __typename?: 'MatchTimedOutRecord' }
    & Pick<MatchTimedOutRecord, 'matchId'>
  )> }
);

export type AcceptFriendshipInvitationMutationVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type AcceptFriendshipInvitationMutation = (
  { __typename?: 'Mutation' }
  & { acceptFriendshipInvitation?: Maybe<(
    { __typename?: 'AcceptFriendshipInvitationPayload' }
    & { query?: Maybe<(
      { __typename?: 'Query' }
      & { friendship?: Maybe<(
        { __typename?: 'Friendship' }
        & Pick<Friendship, 'isAccepted' | 'invitingUserId' | 'blockingUsersIds'>
      )> }
    )> }
  )> }
);

export type BlockUserMutationVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type BlockUserMutation = (
  { __typename?: 'Mutation' }
  & { blockUser?: Maybe<(
    { __typename?: 'BlockUserPayload' }
    & { query?: Maybe<(
      { __typename?: 'Query' }
      & { friendship?: Maybe<(
        { __typename?: 'Friendship' }
        & Pick<Friendship, 'isAccepted' | 'invitingUserId' | 'blockingUsersIds'>
      )> }
    )> }
  )> }
);

export type DeclineFriendshipInvitationMutationVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type DeclineFriendshipInvitationMutation = (
  { __typename?: 'Mutation' }
  & { declineFriendshipInvitation?: Maybe<(
    { __typename?: 'DeclineFriendshipInvitationPayload' }
    & { query?: Maybe<(
      { __typename?: 'Query' }
      & { friendship?: Maybe<(
        { __typename?: 'Friendship' }
        & Pick<Friendship, 'isAccepted' | 'invitingUserId' | 'blockingUsersIds'>
      )> }
    )> }
  )> }
);

export type DeleteFriendshipMutationVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type DeleteFriendshipMutation = (
  { __typename?: 'Mutation' }
  & { deleteFriendship?: Maybe<(
    { __typename?: 'DeleteFriendshipPayload' }
    & Pick<DeleteFriendshipPayload, 'status'>
  )> }
);

export type InviteUserToFriendsListMutationVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type InviteUserToFriendsListMutation = (
  { __typename?: 'Mutation' }
  & { inviteUserToFriendsList?: Maybe<(
    { __typename?: 'InviteUserToFriendsListPayload' }
    & { query?: Maybe<(
      { __typename?: 'Query' }
      & { friendship?: Maybe<(
        { __typename?: 'Friendship' }
        & Pick<Friendship, 'isAccepted' | 'invitingUserId' | 'blockingUsersIds'>
      )> }
    )> }
  )> }
);

export type UnblockUserMutationVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type UnblockUserMutation = (
  { __typename?: 'Mutation' }
  & { unblockUser?: Maybe<(
    { __typename?: 'UnblockUserPayload' }
    & { query?: Maybe<(
      { __typename?: 'Query' }
      & { friendship?: Maybe<(
        { __typename?: 'Friendship' }
        & Pick<Friendship, 'isAccepted' | 'invitingUserId' | 'blockingUsersIds'>
      )> }
    )> }
  )> }
);

export type TeamPlayerFragment = (
  { __typename?: 'MatchPlayer' }
  & Pick<MatchPlayer, 'team' | 'kills' | 'assists' | 'deaths' | 'hs' | 'ratingStart' | 'ratingEnd'>
  & { user?: Maybe<(
    { __typename?: 'User' }
    & Pick<User, 'id' | 'username'>
  )> }
);

export type FriendFragment = (
  { __typename?: 'User' }
  & Pick<User, 'id' | 'username'>
);

export type GetFriendsViewQueryVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type GetFriendsViewQuery = (
  { __typename?: 'Query' }
  & { user?: Maybe<(
    { __typename?: 'User' }
    & { friends?: Maybe<(
      { __typename?: 'UserConnection' }
      & { nodes?: Maybe<Array<(
        { __typename?: 'User' }
        & FriendFragment
      )>> }
    )> }
  )>, blockedUsers?: Maybe<(
    { __typename?: 'UserConnection' }
    & { nodes?: Maybe<Array<(
      { __typename?: 'User' }
      & FriendFragment
    )>> }
  )>, friendshipInvitations?: Maybe<(
    { __typename?: 'UserConnection' }
    & { nodes?: Maybe<Array<(
      { __typename?: 'User' }
      & FriendFragment
    )>> }
  )> }
);

export type GetMatchesQueryVariables = Exact<{
  userId: Scalars['Uuid'];
  mode: Scalars['Int'];
}>;


export type GetMatchesQuery = (
  { __typename?: 'Query' }
  & { matches?: Maybe<(
    { __typename?: 'MatchConnection' }
    & { nodes?: Maybe<Array<(
      { __typename?: 'Match' }
      & Pick<Match, 'id' | 'map' | 'mode' | 'scoreT' | 'scoreCT' | 'winner' | 'createdAt' | 'finishedAt'>
      & { teamTerrorists?: Maybe<Array<(
        { __typename?: 'MatchPlayer' }
        & TeamPlayerFragment
      )>>, teamCounterTerrorists?: Maybe<Array<(
        { __typename?: 'MatchPlayer' }
        & TeamPlayerFragment
      )>> }
    )>> }
  )> }
);

export type GetMatchesViewQueryVariables = Exact<{
  userId: Scalars['Uuid'];
  mode: Scalars['Int'];
}>;


export type GetMatchesViewQuery = (
  { __typename?: 'Query' }
  & { matches?: Maybe<(
    { __typename?: 'MatchConnection' }
    & { nodes?: Maybe<Array<(
      { __typename?: 'Match' }
      & Pick<Match, 'id' | 'map' | 'mode' | 'scoreT' | 'scoreCT' | 'winner' | 'createdAt' | 'finishedAt'>
      & { teamTerrorists?: Maybe<Array<(
        { __typename?: 'MatchPlayer' }
        & TeamPlayerFragment
      )>>, teamCounterTerrorists?: Maybe<Array<(
        { __typename?: 'MatchPlayer' }
        & TeamPlayerFragment
      )>> }
    )>> }
  )>, user?: Maybe<(
    { __typename?: 'User' }
    & { stats?: Maybe<(
      { __typename?: 'PlayerStatsDto' }
      & Pick<PlayerStatsDto, 'matchesTotal' | 'matchesWon' | 'totalHs' | 'totalKills' | 'totalDeaths'>
    )> }
  )> }
);

export type GetProfileViewQueryVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type GetProfileViewQuery = (
  { __typename?: 'Query' }
  & { user?: Maybe<(
    { __typename?: 'User' }
    & Pick<User, 'id' | 'username'>
  )>, friendship?: Maybe<(
    { __typename?: 'Friendship' }
    & Pick<Friendship, 'isAccepted' | 'invitingUserId' | 'blockingUsersIds'>
  )> }
);

export type FriendshipInvitationCreatedSubscriptionVariables = Exact<{ [key: string]: never; }>;


export type FriendshipInvitationCreatedSubscription = (
  { __typename?: 'Subscription' }
  & { friendshipInvitationCreated?: Maybe<(
    { __typename?: 'FriendshipInvitationCreatedPayload' }
    & { record: (
      { __typename?: 'FriendshipInvitationCreatedRecord' }
      & { invitingUser?: Maybe<(
        { __typename?: 'User' }
        & Pick<User, 'id' | 'username'>
      )> }
    ) }
  )> }
);

export type GetUserQueryVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type GetUserQuery = (
  { __typename?: 'Query' }
  & { user?: Maybe<(
    { __typename?: 'User' }
    & Pick<User, 'id' | 'steamId' | 'username'>
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
    

declare module '*/getUsers.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const getUsers: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/LobbyFields.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const LobbyFields: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/acceptLobbyInvitation.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const acceptLobbyInvitation: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/acceptMatch.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const acceptMatch: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/cancelLobbyInvitation.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const cancelLobbyInvitation: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/changeLobbyConfiguration.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const changeLobbyConfiguration: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/createLobby.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const createLobby: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/inviteUserToLobby.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const inviteUserToLobby: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/leaveLobby.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const leaveLobby: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/startSearchingForGame.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const startSearchingForGame: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/getFriends.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const getFriends: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/getLobby.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const getLobby: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/getLobbyInvitations.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const getLobbyInvitations: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/lobbyConfigurationChanged.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const lobbyConfigurationChanged: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/lobbyInvitationAccepted.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const lobbyInvitationAccepted: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/lobbyInvitationCreated.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const lobbyInvitationCreated: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/lobbyMemberLeft.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const lobbyMemberLeft: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/lobbyMemberRoleChanged.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const lobbyMemberRoleChanged: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/lobbyStatusChanged.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const lobbyStatusChanged: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/matchAccepted.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const matchAccepted: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/matchCanceled.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const matchCanceled: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/matchFinished.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const matchFinished: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/matchPlayerLeft.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const matchPlayerLeft: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/matchReady.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const matchReady: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/matchStarted.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const matchStarted: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/matchStarting.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const matchStarting: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/matchTimedOut.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const matchTimedOut: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/acceptFriendshipInvitation.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const acceptFriendshipInvitation: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/blockUser.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const blockUser: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/declineFriendshipInvitation.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const declineFriendshipInvitation: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/deleteFriendship.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const deleteFriendship: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/inviteUserToFriendsList.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const inviteUserToFriendsList: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/unblockUser.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const unblockUser: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/TeamPlayer.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const TeamPlayer: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/getFriendsView.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const Friend: DocumentNode;
export const getFriendsView: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/getMatches.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const getMatches: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/getMatchesView.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const getMatchesView: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/getProfileView.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const getProfileView: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/friendshipInvitationCreated.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const friendshipInvitationCreated: DocumentNode;

  export default defaultDocument;
}
    

declare module '*/Settings.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const getUser: DocumentNode;

  export default defaultDocument;
}
    