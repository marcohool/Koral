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
import { TermsPrompt } from 'pages/auth/signup/SignupPage';
import useSignup from 'pages/auth/signup/useSignup';

const signupPasswordFormProps = {
  defaultValues: {
    password: '',
    passwordConfirm: '',
  },

  resolver: yupResolver(signupPasswordSchema),
};

const SignupPasswordForm: FC<{ email: string }> = ({ email }) => {
  const form = useForm<SignupPasswordFormData>({ ...signupPasswordFormProps });
  const { mutate: signup, isPending } = useSignup();

  const onSubmit = (data: SignupPasswordFormData) => {
    console.log('Signup password form submitted', data, email);
    signup(
      { email, ...data },
      {
        onSuccess: () => console.log('Success'),
        onError: (error) =>
          form.setError('root', {
            type: 'manual',
            message:
              (error.response?.data as string) ??
              `An unexpected error has occurred. Please try again later\n${error.message}`,
          }),
      },
    );
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
                <FormInput
                  placeholder="Enter your password"
                  {...field}
                  type="password"
                />
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
                <FormInput
                  placeholder="Confirm your password"
                  {...field}
                  type="password"
                />
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
