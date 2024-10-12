import * as yup from 'yup';

const loginSchema = yup
  .object({
    email: yup
      .string()
      .email('Must be a valid email')
      .required('Email is required'),
    password: yup
      .string()
      .min(6, 'Password must be at least 6 characters')
      .required('Password is required'),
  })
  .required();

type LoginFormData = yup.InferType<typeof loginSchema>;

export type { LoginFormData };
export default loginSchema;
