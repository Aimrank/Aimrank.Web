export type IFieldErrors = {
  [key: string]: string[];
}

export interface IErrorResponse {
  type: string;
  title: string;
  status: number;
  code?: string;
  errors?: IFieldErrors;
  [key: string]: any;
}

interface IAttributes {
  [key: string]: any;
}

export class ErrorResponse implements IErrorResponse {
  public type: string;
  public title: string;
  public status: number;
  public code?: string;
  public errors?: IFieldErrors;
  public attrs?: IAttributes;

  constructor(type: string, title: string, status: number, code?: string, errors?: IFieldErrors) {
    this.type = type;
    this.title = title;
    this.status = status;
    this.code = code;
    this.errors = errors;
  }

  private setAttrs(attrs: IAttributes) {
    this.attrs = attrs ? { ...attrs } : undefined;
  }

  static create(response: IErrorResponse) {
    const { type, title, status, code, errors, ...attrs } = response;

    const res = new ErrorResponse(
      response.type,
      response.title,
      response.status,
      response.code,
      response.errors
    );

    res.setAttrs(attrs);

    return res;
  }
}
