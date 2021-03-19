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
  username: Scalars['String'];
  stats?: Maybe<PlayerStatsDto>;
  steamId?: Maybe<Scalars['String']>;
  id: Scalars['Uuid'];
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
  matchAccepted?: Maybe<MatchAcceptedEvent>;
  matchReady?: Maybe<MatchReadyPayload>;
  matchStarting?: Maybe<MatchStartingEvent>;
  matchStarted?: Maybe<MatchStartedEvent>;
  matchTimedOut?: Maybe<MatchTimedOutEvent>;
  matchCanceled?: Maybe<MatchCanceledEvent>;
  matchFinished?: Maybe<MatchFinishedEvent>;
  matchPlayerLeft?: Maybe<MatchPlayerLeftEvent>;
  lobbyInvitationAccepted?: Maybe<InvitationAcceptedPayload>;
  lobbyInvitationCanceled?: Maybe<InvitationCanceledPayload>;
  lobbyConfigurationChanged?: Maybe<LobbyConfigurationChangedPayload>;
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

export type PlayerStatsDto = {
  __typename?: 'PlayerStatsDto';
  matchesTotal: Scalars['Int'];
  matchesWon: Scalars['Int'];
  totalKills: Scalars['Int'];
  totalDeaths: Scalars['Int'];
  totalHs: Scalars['Int'];
  modes?: Maybe<Array<Maybe<PlayerStatsModeDto>>>;
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

export type CreateLobbyCommandInput = {
  lobbyId: Scalars['Uuid'];
};

export type AcceptMatchPayload = {
  __typename?: 'AcceptMatchPayload';
  query?: Maybe<Query>;
  status?: Maybe<Scalars['String']>;
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
  query?: Maybe<Query>;
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
  lobbyId: Scalars['Uuid'];
  invitingPlayerId: Scalars['Uuid'];
  invitedPlayerId: Scalars['Uuid'];
};

export type FriendshipInvitationCreatedPayload = {
  __typename?: 'FriendshipInvitationCreatedPayload';
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

export type MatchReadyPayload = {
  __typename?: 'MatchReadyPayload';
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

export type InvitationAcceptedPayload = {
  __typename?: 'InvitationAcceptedPayload';
  lobbyId: Scalars['Uuid'];
  invitedPlayerId: Scalars['Uuid'];
};

export type InvitationCanceledPayload = {
  __typename?: 'InvitationCanceledPayload';
  lobbyId: Scalars['Uuid'];
  invitedPlayerId: Scalars['Uuid'];
};

export type LobbyConfigurationChangedPayload = {
  __typename?: 'LobbyConfigurationChangedPayload';
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

export type PlayerStatsMapDto = {
  __typename?: 'PlayerStatsMapDto';
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
      & { nodes?: Maybe<Array<Maybe<(
        { __typename?: 'User' }
        & FriendFragment
      )>>> }
    )> }
  )>, blockedUsers?: Maybe<(
    { __typename?: 'UserConnection' }
    & { nodes?: Maybe<Array<Maybe<(
      { __typename?: 'User' }
      & FriendFragment
    )>>> }
  )>, friendshipInvitations?: Maybe<(
    { __typename?: 'UserConnection' }
    & { nodes?: Maybe<Array<Maybe<(
      { __typename?: 'User' }
      & FriendFragment
    )>>> }
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
    & { nodes?: Maybe<Array<Maybe<(
      { __typename?: 'Match' }
      & Pick<Match, 'id' | 'map' | 'mode' | 'scoreT' | 'scoreCT' | 'winner' | 'createdAt' | 'finishedAt'>
      & { teamTerrorists?: Maybe<Array<Maybe<(
        { __typename?: 'MatchPlayer' }
        & TeamPlayerFragment
      )>>>, teamCounterTerrorists?: Maybe<Array<Maybe<(
        { __typename?: 'MatchPlayer' }
        & TeamPlayerFragment
      )>>> }
    )>>> }
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
    & { nodes?: Maybe<Array<Maybe<(
      { __typename?: 'Match' }
      & Pick<Match, 'id' | 'map' | 'mode' | 'scoreT' | 'scoreCT' | 'winner' | 'createdAt' | 'finishedAt'>
      & { teamTerrorists?: Maybe<Array<Maybe<(
        { __typename?: 'MatchPlayer' }
        & TeamPlayerFragment
      )>>>, teamCounterTerrorists?: Maybe<Array<Maybe<(
        { __typename?: 'MatchPlayer' }
        & TeamPlayerFragment
      )>>> }
    )>>> }
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
    

declare module '*/Settings.gql' {
  import { DocumentNode } from 'graphql';
  const defaultDocument: DocumentNode;
  export const getUser: DocumentNode;

  export default defaultDocument;
}
    