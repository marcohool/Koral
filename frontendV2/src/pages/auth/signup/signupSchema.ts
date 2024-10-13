import * as yup from 'yup';

const signupSchema = yup
  .object({
    email: yup.string().email('Must be a valid email').required('Required'),
  })
  .required();

type SignupFormData = yup.InferType<typeof signupSchema>;

export type { SignupFormData };
export default signupSchema;
