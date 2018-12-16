export class UserList {
  constructor(public identityId: string,
    public facebookId: string,
    public firstName: string,
    public lastName: string,
    public userName: string,
    public roleId: number,
    public roleName: string,
    public email: string,
    public phoneNumber: string,
    public emailConfirmed: boolean,
    public phoneNumberConfirmed: boolean
  ) { }
}
