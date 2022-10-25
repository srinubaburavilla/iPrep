export class SearchResponse {
  mapperId: number;
  subjectId: number;
  subject: string;
  question: string;
  answer: string;
  constructor(
    mapperId: number,
    subjectId: number,
    subject: string,
    question: string,
    answer: string
  ) {
    this.mapperId = mapperId;
    this.subjectId = subjectId;
    this.subject = subject;
    this.question = question;
    this.answer = answer;
  }
}
