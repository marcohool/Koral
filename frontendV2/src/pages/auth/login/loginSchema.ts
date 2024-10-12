import * as yup from 'yup';

const loginSchema = yup
  .object({
    email: yup.string().email('Must be a valid email').required('Required'),
    password: yup
      .string()
      .min(6, 'Password must be at least 6 characters')
      .required('Required'),
  })
  .required();

type LoginFormData = yup.InferType<typeof loginSchema>;

export type { LoginFormData };
export default loginSchema;
