export class FieldError {
  constructor(
    public rejectedValue: string,
    public codeMessageMap: Map<String,String>
  ) {
  }

}
