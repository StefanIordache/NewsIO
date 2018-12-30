export class News {
  constructor(public id: number,
    public title: string,
    public headline: string,
    public content: string,
    public thumbnailUrl: string,
    public externalUrl: string,
    public categoryId: number,
    public fromRequest: boolean,
    public newsRequestId: number
  ) { }
}
