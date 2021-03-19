import { Ref } from "vue";
import { useQuery } from "~/graphql/useQuery";
import { GetUsersQuery, GetUsersQueryVariables } from "~/graphql/types/types";

import GET_USERS from "./query/getUsers.gql";

export const useGetUsers = (
  username: string | Ref<string>
) => useQuery<
  GetUsersQuery,
  GetUsersQueryVariables>({
    query: GET_USERS,
    variables: {
      // @ts-ignore
      username
    }
  }, true);