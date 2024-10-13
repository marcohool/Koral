import { FC } from 'react';
import { useForm } from 'react-hook-form';
import {
  SignupPasswordFormData,
  signupPasswordSchema,
} from 'pages/auth/signup/signupEmailSchema';
import { yupResolver } from '@hookform/resolvers/yup';
import AuthForm from 'pages/auth/AuthForm';
import {
  FormControl,
  FormField,
  FormInput,
  FormItem,
  FormLabel,
  FormMessage,
} from 'components/form/Form';
import useLogin from 'pages/auth/login/useLogin';
import { TermsPrompt } from 'pages/auth/signup/SignupPage';

const signupPasswordFormProps = {
  defaultValues: {
    password: '',
    passwordConfirm: '',
  },

  resolver: yupResolver(signupPasswordSchema),
};

const SignupPasswordForm: FC<{ email: string }> = ({ email }) => {
  const form = useForm<SignupPasswordFormData>({ ...signupPasswordFormProps });
  const { isPending } = useLogin(); // Change this

  const onSubmit = (data: SignupPasswordFormData) => {
    console.log('Signup password form submitted', data, email);
  };

  return (
    <>
      <AuthForm
        form={form}
        heading={{
          title: 'Create your account',
          subtitle: 'Enter a password to create your account',
        }}
        submitText="Sign up"
        onSubmit={onSubmit}
        isPending={isPending}
        displayThirdPartyAuth={false}
      >
        <FormField
          control={form.control}
          name="password"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Password</FormLabel>
              <FormControl>
                <FormInput placeholder="Enter your password" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="passwordConfirm"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Confirm password</FormLabel>
              <FormControl>
                <FormInput placeholder="Confirm your password" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
      </AuthForm>
      <TermsPrompt />
    </>
  );
};

export default SignupPasswordForm;
