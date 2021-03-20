import { GraphQLError } from "graphql";

export type IFieldErrors = {
  [key: string]: string[];
}

export interface IErrorResponse {
  title: string;
  code?: string;
  errors?: IFieldErrors;
}

export class ErrorResponse implements IErrorResponse {
  public title: string;
  public code?: string;
  public errors: IFieldErrors = {};

  constructor(title: string, code?: string) {
    this.title = title;
    this.code = code;
  }

  static fromGraphQLError(error: GraphQLError) {
    if (error.extensions) {
      const errorResponse = new ErrorResponse(
        error.extensions.message,
        error.extensions.code);

      if (error.extensions.errors) {
        errorResponse.errors = JSON.parse(error.extensions.errors);
      }

      return errorResponse;
    }

    return new ErrorResponse(error.message);
  }
}
