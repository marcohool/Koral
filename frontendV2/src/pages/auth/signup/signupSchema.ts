import * as yup from 'yup';

const signupSchema = yup
  .object({
    email: yup.string().email('Must be a valid email').required('Required'),
  })
  .required();

const signupPasswordSchema = yup.object({
  password: yup
    .string()
    .min(6, 'Password must be at least 6 characters')
    .required('Password is required'),
  passwordConfirm: yup
    .string()
    .oneOf([yup.ref('password')], 'Passwords must match')
    .required('Password confirmation is required'),
});

type SignupEmailFormData = yup.InferType<typeof signupSchema>;
type SignupPasswordFormData = yup.InferType<typeof signupPasswordSchema>;

export type { SignupEmailFormData, SignupPasswordFormData };
export { signupSchema, signupPasswordSchema };
