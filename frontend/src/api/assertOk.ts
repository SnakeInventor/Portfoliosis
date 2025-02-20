// import MyHTTPError for extention to work

declare global {
  interface Promise<T> {
      assertOk(additionalMessage?: string): Promise<T>;
  }
}

// eslint-disable-next-line @typescript-eslint/no-unused-vars
Promise.prototype.assertOk = function<T>(this: Promise<Response>, additionalMessage?: string): Promise<Response> {
  return this.then((response: Response) => {
    if (!response.ok) {
      throw new MyHTTPError(
        additionalMessage
          ? `HTTP error: server responded with ${response.status}! Message: ${additionalMessage}`
          : `HTTP error: server responded with ${response.status}!`,
        response
      );
    }
    return response;
  });
};

export class MyHTTPError extends Error {
  url: string;
  status: number;
  statusText: string;
  response: Response;
  constructor(message: string, response: Response) {
    super(message);
    this.name = "MyHTTPError";
    this.url = response.url;
    this.status = response.status;
    this.statusText = response.statusText;
    this.response = response;
  }
}