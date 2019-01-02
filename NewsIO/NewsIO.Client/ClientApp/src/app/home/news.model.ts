export class News {
  constructor(public id: number,
    public title: string,
    public headline: string,
    public content: string,
    public thumbnailUrl: string,
    public externalUrl: string,
    public categoryId: number,
    public fromRequest: boolean,
    public newsRequestId: number,
    public publishDate: Date,
    public publishedById: string,
    public publishedBy: string,
    public lastEditDate: Date,
    public lastEditedyById: string,
    public lastEditedBy: string
  ) { }
}
