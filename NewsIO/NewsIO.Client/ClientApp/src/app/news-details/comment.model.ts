export class Coment {
  constructor(public id: number,
    public commentText:string,
    public newsId: number,
    public publishDate: Date,
    public publishedById:string,
    public publishedBy: string,
    public lastEditDate :Date,
    public lastEditedyById:string,
    public lastEditedBy:string
  ) { }
}
