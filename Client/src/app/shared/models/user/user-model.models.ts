import { BaseModel } from 'src/app/shared/models/base/base-model.models';

export class UserModel extends BaseModel {
  id: number;
  name: string;
  login: string;
  password: string;
}
