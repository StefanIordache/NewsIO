export class Signup {
  constructor(public id: number,
    public email: string,
    public username: string,
    public password: string,
    public firstName: string,
    public lastName: string,
    public location: string,
    accessTokens: any[]
  ) { }
}
