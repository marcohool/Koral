import * as yup from 'yup';

const signupEmailSchema = yup
  .object({
    email: yup.string().email('Must be a valid email').required('Required'),
  })
  .required();

const signupPasswordSchema = yup
  .object({
    password: yup.string().required('Required'),
    passwordConfirm: yup.string().required('Required'),
  })
  .required();

type SignupEmailFormData = yup.InferType<typeof signupEmailSchema>;
type SignupPasswordFormData = yup.InferType<typeof signupPasswordSchema>;

export type { SignupEmailFormData, SignupPasswordFormData };
export { signupEmailSchema, signupPasswordSchema };
