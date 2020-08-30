export class ValidationConstants {
  public static readonly passwordPattern = '^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&_-])[A-Za-z\d$@$!%*?&].{8,}$';
  public static readonly textPattern = '^(?=.*[a-z])(?=.*[A-Z])[A-Za-z\d]{3,}$';
}
