export type AsyncResult<T, TError> = Promise<Result<T, TError>>;

export class Result<T, TError> {
  private _ok: boolean = true;

  private constructor(
    private readonly _value?: T,
    private readonly _error?: TError) {}

  isOk() {
    return this._ok;
  }

  static success<T, TError>(value?: T) {
    const result = new Result<T, TError>(value);
    result._ok = true;
    return result;
  }

  static fail<T, TError>(error?: TError) {
    const result = new Result<T, TError>(undefined, error);
    result._ok = false;
    return result;
  }

  get value(): T {
    return this._value!;
  }

  get error(): TError {
    return this._error!;
  }
}
