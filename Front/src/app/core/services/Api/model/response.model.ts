export interface ResponseModel<model> {
  success: boolean;
  errorMessage: string;
  message: String;
  data: model;
}
