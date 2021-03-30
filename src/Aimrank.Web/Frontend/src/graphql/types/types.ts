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

export type LobbyConfigurationChangedRecord = {
  __typename?: 'LobbyConfigurationChangedRecord';
  name: Scalars['String'];
  maps: Array<Scalars['String']>;
  lobbyId: Scalars['Uuid'];
  mode: Scalars['Int'];
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
  maps: Array<Scalars['String']>;
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
  changeLobbyConfiguration?: Maybe<ChangeLobbyConfigurationPayload>;
  invitePlayerToLobby?: Maybe<InvitePlayerToLobbyPayload>;
  kickPlayerFromLobby?: Maybe<KickPlayerFromLobbyPayload>;
  acceptLobbyInvitation?: Maybe<AcceptLobbyInvitationPayload>;
  cancelLobbyInvitation?: Maybe<CancelLobbyInvitationPayload>;
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
  input: AuthenticateCommandInput;
};


export type MutationSignUpArgs = {
  input: RegisterNewUserCommandInput;
};


export type MutationChangeLobbyConfigurationArgs = {
  input: ChangeLobbyConfigurationCommandInput;
};


export type MutationInvitePlayerToLobbyArgs = {
  input: InvitePlayerToLobbyCommandInput;
};


export type MutationKickPlayerFromLobbyArgs = {
  input: KickPlayerFromLobbyCommandInput;
};


export type MutationAcceptLobbyInvitationArgs = {
  input: AcceptLobbyInvitationCommandInput;
};


export type MutationCancelLobbyInvitationArgs = {
  input: CancelLobbyInvitationCommandInput;
};


export type MutationLeaveLobbyArgs = {
  input: LeaveLobbyCommandInput;
};


export type MutationStartSearchingForGameArgs = {
  input: StartSearchingForGameCommandInput;
};


export type MutationCancelSearchingForGameArgs = {
  input: CancelSearchingForGameCommandInput;
};


export type MutationAcceptMatchArgs = {
  input: AcceptMatchCommandInput;
};


export type MutationInviteUserToFriendsListArgs = {
  input: InviteUserToFriendsListCommandInput;
};


export type MutationAcceptFriendshipInvitationArgs = {
  input: AcceptFriendshipInvitationCommandInput;
};


export type MutationDeclineFriendshipInvitationArgs = {
  input: DeclineFriendshipInvitationCommandInput;
};


export type MutationBlockUserArgs = {
  input: BlockUserCommandInput;
};


export type MutationUnblockUserArgs = {
  input: UnblockUserCommandInput;
};


export type MutationDeleteFriendshipArgs = {
  input: DeleteFriendshipCommandInput;
};

export type Subscription = {
  __typename?: 'Subscription';
  lobbyInvitationCreated?: Maybe<LobbyInvitationCreatedPayload>;
  friendshipInvitationCreated?: Maybe<FriendshipInvitationCreatedPayload>;
  matchAccepted?: Maybe<MatchAcceptedPayload>;
  matchReady?: Maybe<MatchReadyPayload>;
  matchStarting?: Maybe<MatchStartingPayload>;
  matchStarted?: Maybe<MatchStartedPayload>;
  matchTimedOut?: Maybe<MatchTimedOutPayload>;
  matchCanceled?: Maybe<MatchCanceledPayload>;
  matchFinished?: Maybe<MatchFinishedPayload>;
  matchPlayerLeft?: Maybe<MatchPlayerLeftPayload>;
  lobbyInvitationAccepted?: Maybe<LobbyInvitationAcceptedPayload>;
  lobbyConfigurationChanged?: Maybe<LobbyConfigurationChangedPayload>;
  lobbyStatusChanged?: Maybe<LobbyStatusChangedPayload>;
  lobbyMemberLeft?: Maybe<LobbyMemberLeftPayload>;
  lobbyMemberRoleChanged?: Maybe<LobbyMemberRoleChangedPayload>;
  lobbyMemberKicked?: Maybe<LobbyMemberKickedPayload>;
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


export type SubscriptionLobbyMemberKickedArgs = {
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

export type StartSearchingForGamePayload = {
  __typename?: 'StartSearchingForGamePayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type LeaveLobbyPayload = {
  __typename?: 'LeaveLobbyPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type CancelLobbyInvitationPayload = {
  __typename?: 'CancelLobbyInvitationPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type AcceptLobbyInvitationPayload = {
  __typename?: 'AcceptLobbyInvitationPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type KickPlayerFromLobbyPayload = {
  __typename?: 'KickPlayerFromLobbyPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type InvitePlayerToLobbyPayload = {
  __typename?: 'InvitePlayerToLobbyPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type ChangeLobbyConfigurationPayload = {
  __typename?: 'ChangeLobbyConfigurationPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type CreateLobbyPayload = {
  __typename?: 'CreateLobbyPayload';
  record: Lobby;
  query?: Maybe<Query>;
  recordId: Scalars['Uuid'];
  status: Scalars['String'];
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
  status: Scalars['String'];
};

export type SignUpPayload = {
  __typename?: 'SignUpPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type SignInPayload = {
  __typename?: 'SignInPayload';
  query?: Maybe<Query>;
  record?: Maybe<AuthenticationSuccessRecord>;
  status: Scalars['String'];
};

export type CancelSearchingForGamePayload = {
  __typename?: 'CancelSearchingForGamePayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type AcceptMatchPayload = {
  __typename?: 'AcceptMatchPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type ChangeLobbyConfigurationCommandInput = {
  lobbyId: Scalars['Uuid'];
  name?: Maybe<Scalars['String']>;
  mode: Scalars['Int'];
  maps?: Maybe<Array<Maybe<Scalars['String']>>>;
};

export type InvitePlayerToLobbyCommandInput = {
  lobbyId: Scalars['Uuid'];
  invitedPlayerId: Scalars['Uuid'];
};

export type KickPlayerFromLobbyCommandInput = {
  lobbyId: Scalars['Uuid'];
  playerId: Scalars['Uuid'];
};

export type AcceptLobbyInvitationCommandInput = {
  lobbyId: Scalars['Uuid'];
};

export type CancelLobbyInvitationCommandInput = {
  lobbyId: Scalars['Uuid'];
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
  status: Scalars['String'];
};

export type AcceptFriendshipInvitationPayload = {
  __typename?: 'AcceptFriendshipInvitationPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type DeclineFriendshipInvitationPayload = {
  __typename?: 'DeclineFriendshipInvitationPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type BlockUserPayload = {
  __typename?: 'BlockUserPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type UnblockUserPayload = {
  __typename?: 'UnblockUserPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
};

export type DeleteFriendshipPayload = {
  __typename?: 'DeleteFriendshipPayload';
  query?: Maybe<Query>;
  status: Scalars['String'];
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

export type MatchTimedOutPayload = {
  __typename?: 'MatchTimedOutPayload';
  query?: Maybe<Query>;
  record: MatchTimedOutRecord;
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

export type LobbyMemberKickedPayload = {
  __typename?: 'LobbyMemberKickedPayload';
  query?: Maybe<Query>;
  record: LobbyMemberKickedRecord;
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

export type LobbyMemberKickedRecord = {
  __typename?: 'LobbyMemberKickedRecord';
  lobbyId: Scalars['Uuid'];
  playerId: Scalars['Uuid'];
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

export type MatchTimedOutRecord = {
  __typename?: 'MatchTimedOutRecord';
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
  input: AuthenticateCommandInput;
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
  input: RegisterNewUserCommandInput;
}>;


export type SignUpMutation = (
  { __typename?: 'Mutation' }
  & { signUp?: Maybe<(
    { __typename?: 'SignUpPayload' }
    & Pick<SignUpPayload, 'status'>
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
    & Pick<LobbyConfiguration, 'maps' | 'mode' | 'name'>
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

export type InvitePlayerToLobbyMutationVariables = Exact<{
  lobbyId: Scalars['Uuid'];
  playerId: Scalars['Uuid'];
}>;


export type InvitePlayerToLobbyMutation = (
  { __typename?: 'Mutation' }
  & { invitePlayerToLobby?: Maybe<(
    { __typename?: 'InvitePlayerToLobbyPayload' }
    & Pick<InvitePlayerToLobbyPayload, 'status'>
  )> }
);

export type KickPlayerFromLobbyMutationVariables = Exact<{
  lobbyId: Scalars['Uuid'];
  playerId: Scalars['Uuid'];
}>;


export type KickPlayerFromLobbyMutation = (
  { __typename?: 'Mutation' }
  & { kickPlayerFromLobby?: Maybe<(
    { __typename?: 'KickPlayerFromLobbyPayload' }
    & Pick<KickPlayerFromLobbyPayload, 'status'>
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
      & Pick<LobbyConfigurationChangedRecord, 'lobbyId' | 'maps' | 'mode' | 'name'>
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

export type LobbyMemberKickedSubscriptionVariables = Exact<{
  lobbyId: Scalars['Uuid'];
}>;


export type LobbyMemberKickedSubscription = (
  { __typename?: 'Subscription' }
  & { lobbyMemberKicked?: Maybe<(
    { __typename?: 'LobbyMemberKickedPayload' }
    & { record: (
      { __typename?: 'LobbyMemberKickedRecord' }
      & Pick<LobbyMemberKickedRecord, 'lobbyId' | 'playerId'>
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
    { __typename?: 'MatchTimedOutPayload' }
    & { record: (
      { __typename?: 'MatchTimedOutRecord' }
      & Pick<MatchTimedOutRecord, 'matchId'>
    ) }
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

export type GetSettingsViewQueryVariables = Exact<{
  userId: Scalars['Uuid'];
}>;


export type GetSettingsViewQuery = (
  { __typename?: 'Query' }
  & { user?: Maybe<(
    { __typename?: 'User' }
    & Pick<User, 'id' | 'steamId' | 'username'>
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

import { Ref } from "vue";
import { apolloClient } from "~/graphql/apolloClient";
import { useQuery, useMutation, useSubscription, UseQueryOptions, UseMutationOptions, UseSubscriptionOptions } from "~/graphql/hooks";
import SIGN_IN from "../../modules/authentication/graphql/mutations/signIn.gql";
import SIGN_OUT from "../../modules/authentication/graphql/mutations/signOut.gql";
import SIGN_UP from "../../modules/authentication/graphql/mutations/signUp.gql";
import GET_USERS from "../../modules/common/graphql/query/getUsers.gql";
import ACCEPT_LOBBY_INVITATION from "../../modules/lobby/graphql/mutations/acceptLobbyInvitation.gql";
import ACCEPT_MATCH from "../../modules/lobby/graphql/mutations/acceptMatch.gql";
import CANCEL_LOBBY_INVITATION from "../../modules/lobby/graphql/mutations/cancelLobbyInvitation.gql";
import CHANGE_LOBBY_CONFIGURATION from "../../modules/lobby/graphql/mutations/changeLobbyConfiguration.gql";
import CREATE_LOBBY from "../../modules/lobby/graphql/mutations/createLobby.gql";
import INVITE_PLAYER_TO_LOBBY from "../../modules/lobby/graphql/mutations/invitePlayerToLobby.gql";
import KICK_PLAYER_FROM_LOBBY from "../../modules/lobby/graphql/mutations/kickPlayerFromLobby.gql";
import LEAVE_LOBBY from "../../modules/lobby/graphql/mutations/leaveLobby.gql";
import START_SEARCHING_FOR_GAME from "../../modules/lobby/graphql/mutations/startSearchingForGame.gql";
import GET_FRIENDS from "../../modules/lobby/graphql/query/getFriends.gql";
import GET_LOBBY from "../../modules/lobby/graphql/query/getLobby.gql";
import GET_LOBBY_INVITATIONS from "../../modules/lobby/graphql/query/getLobbyInvitations.gql";
import LOBBY_CONFIGURATION_CHANGED from "../../modules/lobby/graphql/subscriptions/lobbyConfigurationChanged.gql";
import LOBBY_INVITATION_ACCEPTED from "../../modules/lobby/graphql/subscriptions/lobbyInvitationAccepted.gql";
import LOBBY_INVITATION_CREATED from "../../modules/lobby/graphql/subscriptions/lobbyInvitationCreated.gql";
import LOBBY_MEMBER_KICKED from "../../modules/lobby/graphql/subscriptions/lobbyMemberKicked.gql";
import LOBBY_MEMBER_LEFT from "../../modules/lobby/graphql/subscriptions/lobbyMemberLeft.gql";
import LOBBY_MEMBER_ROLE_CHANGED from "../../modules/lobby/graphql/subscriptions/lobbyMemberRoleChanged.gql";
import LOBBY_STATUS_CHANGED from "../../modules/lobby/graphql/subscriptions/lobbyStatusChanged.gql";
import MATCH_ACCEPTED from "../../modules/lobby/graphql/subscriptions/matchAccepted.gql";
import MATCH_CANCELED from "../../modules/lobby/graphql/subscriptions/matchCanceled.gql";
import MATCH_FINISHED from "../../modules/lobby/graphql/subscriptions/matchFinished.gql";
import MATCH_PLAYER_LEFT from "../../modules/lobby/graphql/subscriptions/matchPlayerLeft.gql";
import MATCH_READY from "../../modules/lobby/graphql/subscriptions/matchReady.gql";
import MATCH_STARTED from "../../modules/lobby/graphql/subscriptions/matchStarted.gql";
import MATCH_STARTING from "../../modules/lobby/graphql/subscriptions/matchStarting.gql";
import MATCH_TIMED_OUT from "../../modules/lobby/graphql/subscriptions/matchTimedOut.gql";
import ACCEPT_FRIENDSHIP_INVITATION from "../../modules/profile/graphql/mutations/acceptFriendshipInvitation.gql";
import BLOCK_USER from "../../modules/profile/graphql/mutations/blockUser.gql";
import DECLINE_FRIENDSHIP_INVITATION from "../../modules/profile/graphql/mutations/declineFriendshipInvitation.gql";
import DELETE_FRIENDSHIP from "../../modules/profile/graphql/mutations/deleteFriendship.gql";
import INVITE_USER_TO_FRIENDS_LIST from "../../modules/profile/graphql/mutations/inviteUserToFriendsList.gql";
import UNBLOCK_USER from "../../modules/profile/graphql/mutations/unblockUser.gql";
import GET_FRIENDS_VIEW from "../../modules/profile/graphql/query/getFriendsView.gql";
import GET_MATCHES from "../../modules/profile/graphql/query/getMatches.gql";
import GET_MATCHES_VIEW from "../../modules/profile/graphql/query/getMatchesView.gql";
import GET_PROFILE_VIEW from "../../modules/profile/graphql/query/getProfileView.gql";
import GET_SETTINGS_VIEW from "../../modules/profile/graphql/query/getSettingsView.gql";
import FRIENDSHIP_INVITATION_CREATED from "../../modules/profile/graphql/subscriptions/friendshipInvitationCreated.gql";

type RefWrapper<T extends object> = Record<keyof T, T[keyof T] | Ref<T[keyof T]>>;

export const useSignIn = (options?: Omit<UseMutationOptions<SignInMutationVariables>, "mutation">) => useMutation<SignInMutation, SignInMutationVariables>(apolloClient, { ...(options ?? {}), mutation: SIGN_IN });
export const useSignOut = (options?: Omit<UseMutationOptions<SignOutMutationVariables>, "mutation">) => useMutation<SignOutMutation, SignOutMutationVariables>(apolloClient, { ...(options ?? {}), mutation: SIGN_OUT });
export const useSignUp = (options?: Omit<UseMutationOptions<SignUpMutationVariables>, "mutation">) => useMutation<SignUpMutation, SignUpMutationVariables>(apolloClient, { ...(options ?? {}), mutation: SIGN_UP });
export const useGetUsers = (options?: Omit<UseQueryOptions<RefWrapper<GetUsersQueryVariables>>, "query">) => useQuery<GetUsersQuery, RefWrapper<GetUsersQueryVariables>>(apolloClient, { ...(options ?? {}), query: GET_USERS });
export const useAcceptLobbyInvitation = (options?: Omit<UseMutationOptions<AcceptLobbyInvitationMutationVariables>, "mutation">) => useMutation<AcceptLobbyInvitationMutation, AcceptLobbyInvitationMutationVariables>(apolloClient, { ...(options ?? {}), mutation: ACCEPT_LOBBY_INVITATION });
export const useAcceptMatch = (options?: Omit<UseMutationOptions<AcceptMatchMutationVariables>, "mutation">) => useMutation<AcceptMatchMutation, AcceptMatchMutationVariables>(apolloClient, { ...(options ?? {}), mutation: ACCEPT_MATCH });
export const useCancelLobbyInvitation = (options?: Omit<UseMutationOptions<CancelLobbyInvitationMutationVariables>, "mutation">) => useMutation<CancelLobbyInvitationMutation, CancelLobbyInvitationMutationVariables>(apolloClient, { ...(options ?? {}), mutation: CANCEL_LOBBY_INVITATION });
export const useChangeLobbyConfiguration = (options?: Omit<UseMutationOptions<ChangeLobbyConfigurationMutationVariables>, "mutation">) => useMutation<ChangeLobbyConfigurationMutation, ChangeLobbyConfigurationMutationVariables>(apolloClient, { ...(options ?? {}), mutation: CHANGE_LOBBY_CONFIGURATION });
export const useCreateLobby = (options?: Omit<UseMutationOptions<CreateLobbyMutationVariables>, "mutation">) => useMutation<CreateLobbyMutation, CreateLobbyMutationVariables>(apolloClient, { ...(options ?? {}), mutation: CREATE_LOBBY });
export const useInvitePlayerToLobby = (options?: Omit<UseMutationOptions<InvitePlayerToLobbyMutationVariables>, "mutation">) => useMutation<InvitePlayerToLobbyMutation, InvitePlayerToLobbyMutationVariables>(apolloClient, { ...(options ?? {}), mutation: INVITE_PLAYER_TO_LOBBY });
export const useKickPlayerFromLobby = (options?: Omit<UseMutationOptions<KickPlayerFromLobbyMutationVariables>, "mutation">) => useMutation<KickPlayerFromLobbyMutation, KickPlayerFromLobbyMutationVariables>(apolloClient, { ...(options ?? {}), mutation: KICK_PLAYER_FROM_LOBBY });
export const useLeaveLobby = (options?: Omit<UseMutationOptions<LeaveLobbyMutationVariables>, "mutation">) => useMutation<LeaveLobbyMutation, LeaveLobbyMutationVariables>(apolloClient, { ...(options ?? {}), mutation: LEAVE_LOBBY });
export const useStartSearchingForGame = (options?: Omit<UseMutationOptions<StartSearchingForGameMutationVariables>, "mutation">) => useMutation<StartSearchingForGameMutation, StartSearchingForGameMutationVariables>(apolloClient, { ...(options ?? {}), mutation: START_SEARCHING_FOR_GAME });
export const useGetFriends = (options?: Omit<UseQueryOptions<RefWrapper<GetFriendsQueryVariables>>, "query">) => useQuery<GetFriendsQuery, RefWrapper<GetFriendsQueryVariables>>(apolloClient, { ...(options ?? {}), query: GET_FRIENDS });
export const useGetLobby = (options?: Omit<UseQueryOptions<RefWrapper<GetLobbyQueryVariables>>, "query">) => useQuery<GetLobbyQuery, RefWrapper<GetLobbyQueryVariables>>(apolloClient, { ...(options ?? {}), query: GET_LOBBY });
export const useGetLobbyInvitations = (options?: Omit<UseQueryOptions<RefWrapper<GetLobbyInvitationsQueryVariables>>, "query">) => useQuery<GetLobbyInvitationsQuery, RefWrapper<GetLobbyInvitationsQueryVariables>>(apolloClient, { ...(options ?? {}), query: GET_LOBBY_INVITATIONS });
export const useLobbyConfigurationChanged = (options?: Omit<UseSubscriptionOptions<RefWrapper<LobbyConfigurationChangedSubscriptionVariables>>, "query">) => useSubscription<LobbyConfigurationChangedSubscription, RefWrapper<LobbyConfigurationChangedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: LOBBY_CONFIGURATION_CHANGED });
export const useLobbyInvitationAccepted = (options?: Omit<UseSubscriptionOptions<RefWrapper<LobbyInvitationAcceptedSubscriptionVariables>>, "query">) => useSubscription<LobbyInvitationAcceptedSubscription, RefWrapper<LobbyInvitationAcceptedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: LOBBY_INVITATION_ACCEPTED });
export const useLobbyInvitationCreated = (options?: Omit<UseSubscriptionOptions<RefWrapper<LobbyInvitationCreatedSubscriptionVariables>>, "query">) => useSubscription<LobbyInvitationCreatedSubscription, RefWrapper<LobbyInvitationCreatedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: LOBBY_INVITATION_CREATED });
export const useLobbyMemberKicked = (options?: Omit<UseSubscriptionOptions<RefWrapper<LobbyMemberKickedSubscriptionVariables>>, "query">) => useSubscription<LobbyMemberKickedSubscription, RefWrapper<LobbyMemberKickedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: LOBBY_MEMBER_KICKED });
export const useLobbyMemberLeft = (options?: Omit<UseSubscriptionOptions<RefWrapper<LobbyMemberLeftSubscriptionVariables>>, "query">) => useSubscription<LobbyMemberLeftSubscription, RefWrapper<LobbyMemberLeftSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: LOBBY_MEMBER_LEFT });
export const useLobbyMemberRoleChanged = (options?: Omit<UseSubscriptionOptions<RefWrapper<LobbyMemberRoleChangedSubscriptionVariables>>, "query">) => useSubscription<LobbyMemberRoleChangedSubscription, RefWrapper<LobbyMemberRoleChangedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: LOBBY_MEMBER_ROLE_CHANGED });
export const useLobbyStatusChanged = (options?: Omit<UseSubscriptionOptions<RefWrapper<LobbyStatusChangedSubscriptionVariables>>, "query">) => useSubscription<LobbyStatusChangedSubscription, RefWrapper<LobbyStatusChangedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: LOBBY_STATUS_CHANGED });
export const useMatchAccepted = (options?: Omit<UseSubscriptionOptions<RefWrapper<MatchAcceptedSubscriptionVariables>>, "query">) => useSubscription<MatchAcceptedSubscription, RefWrapper<MatchAcceptedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: MATCH_ACCEPTED });
export const useMatchCanceled = (options?: Omit<UseSubscriptionOptions<RefWrapper<MatchCanceledSubscriptionVariables>>, "query">) => useSubscription<MatchCanceledSubscription, RefWrapper<MatchCanceledSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: MATCH_CANCELED });
export const useMatchFinished = (options?: Omit<UseSubscriptionOptions<RefWrapper<MatchFinishedSubscriptionVariables>>, "query">) => useSubscription<MatchFinishedSubscription, RefWrapper<MatchFinishedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: MATCH_FINISHED });
export const useMatchPlayerLeft = (options?: Omit<UseSubscriptionOptions<RefWrapper<MatchPlayerLeftSubscriptionVariables>>, "query">) => useSubscription<MatchPlayerLeftSubscription, RefWrapper<MatchPlayerLeftSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: MATCH_PLAYER_LEFT });
export const useMatchReady = (options?: Omit<UseSubscriptionOptions<RefWrapper<MatchReadySubscriptionVariables>>, "query">) => useSubscription<MatchReadySubscription, RefWrapper<MatchReadySubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: MATCH_READY });
export const useMatchStarted = (options?: Omit<UseSubscriptionOptions<RefWrapper<MatchStartedSubscriptionVariables>>, "query">) => useSubscription<MatchStartedSubscription, RefWrapper<MatchStartedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: MATCH_STARTED });
export const useMatchStarting = (options?: Omit<UseSubscriptionOptions<RefWrapper<MatchStartingSubscriptionVariables>>, "query">) => useSubscription<MatchStartingSubscription, RefWrapper<MatchStartingSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: MATCH_STARTING });
export const useMatchTimedOut = (options?: Omit<UseSubscriptionOptions<RefWrapper<MatchTimedOutSubscriptionVariables>>, "query">) => useSubscription<MatchTimedOutSubscription, RefWrapper<MatchTimedOutSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: MATCH_TIMED_OUT });
export const useAcceptFriendshipInvitation = (options?: Omit<UseMutationOptions<AcceptFriendshipInvitationMutationVariables>, "mutation">) => useMutation<AcceptFriendshipInvitationMutation, AcceptFriendshipInvitationMutationVariables>(apolloClient, { ...(options ?? {}), mutation: ACCEPT_FRIENDSHIP_INVITATION });
export const useBlockUser = (options?: Omit<UseMutationOptions<BlockUserMutationVariables>, "mutation">) => useMutation<BlockUserMutation, BlockUserMutationVariables>(apolloClient, { ...(options ?? {}), mutation: BLOCK_USER });
export const useDeclineFriendshipInvitation = (options?: Omit<UseMutationOptions<DeclineFriendshipInvitationMutationVariables>, "mutation">) => useMutation<DeclineFriendshipInvitationMutation, DeclineFriendshipInvitationMutationVariables>(apolloClient, { ...(options ?? {}), mutation: DECLINE_FRIENDSHIP_INVITATION });
export const useDeleteFriendship = (options?: Omit<UseMutationOptions<DeleteFriendshipMutationVariables>, "mutation">) => useMutation<DeleteFriendshipMutation, DeleteFriendshipMutationVariables>(apolloClient, { ...(options ?? {}), mutation: DELETE_FRIENDSHIP });
export const useInviteUserToFriendsList = (options?: Omit<UseMutationOptions<InviteUserToFriendsListMutationVariables>, "mutation">) => useMutation<InviteUserToFriendsListMutation, InviteUserToFriendsListMutationVariables>(apolloClient, { ...(options ?? {}), mutation: INVITE_USER_TO_FRIENDS_LIST });
export const useUnblockUser = (options?: Omit<UseMutationOptions<UnblockUserMutationVariables>, "mutation">) => useMutation<UnblockUserMutation, UnblockUserMutationVariables>(apolloClient, { ...(options ?? {}), mutation: UNBLOCK_USER });
export const useGetFriendsView = (options?: Omit<UseQueryOptions<RefWrapper<GetFriendsViewQueryVariables>>, "query">) => useQuery<GetFriendsViewQuery, RefWrapper<GetFriendsViewQueryVariables>>(apolloClient, { ...(options ?? {}), query: GET_FRIENDS_VIEW });
export const useGetMatches = (options?: Omit<UseQueryOptions<RefWrapper<GetMatchesQueryVariables>>, "query">) => useQuery<GetMatchesQuery, RefWrapper<GetMatchesQueryVariables>>(apolloClient, { ...(options ?? {}), query: GET_MATCHES });
export const useGetMatchesView = (options?: Omit<UseQueryOptions<RefWrapper<GetMatchesViewQueryVariables>>, "query">) => useQuery<GetMatchesViewQuery, RefWrapper<GetMatchesViewQueryVariables>>(apolloClient, { ...(options ?? {}), query: GET_MATCHES_VIEW });
export const useGetProfileView = (options?: Omit<UseQueryOptions<RefWrapper<GetProfileViewQueryVariables>>, "query">) => useQuery<GetProfileViewQuery, RefWrapper<GetProfileViewQueryVariables>>(apolloClient, { ...(options ?? {}), query: GET_PROFILE_VIEW });
export const useGetSettingsView = (options?: Omit<UseQueryOptions<RefWrapper<GetSettingsViewQueryVariables>>, "query">) => useQuery<GetSettingsViewQuery, RefWrapper<GetSettingsViewQueryVariables>>(apolloClient, { ...(options ?? {}), query: GET_SETTINGS_VIEW });
export const useFriendshipInvitationCreated = (options?: Omit<UseSubscriptionOptions<RefWrapper<FriendshipInvitationCreatedSubscriptionVariables>>, "query">) => useSubscription<FriendshipInvitationCreatedSubscription, RefWrapper<FriendshipInvitationCreatedSubscriptionVariables>>(apolloClient, { ...(options ?? {}), query: FRIENDSHIP_INVITATION_CREATED });