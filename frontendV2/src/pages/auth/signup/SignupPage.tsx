import { FC } from 'react';
import AuthLayout from '../AuthLayout';
import AuthForm from 'pages/auth/AuthForm';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import signupSchema, { SignupFormData } from 'pages/auth/signup/signupSchema';
import {
  FormControl,
  FormField,
  FormInput,
  FormItem,
  FormLabel,
  FormMessage,
} from 'components/form/Form';
import useLogin from 'pages/auth/login/useLogin';
import RedirectPrompt from 'pages/auth/RedirectPrompt';
import { Link } from 'react-router-dom';

const TermsPrompt = () => {
  return (
    <p className="px-5 pt-7 text-center text-sm text-muted-foreground-light">
      By clicking continue, you agree to our{' '}
      <Link
        to="/terms"
        className="underline underline-offset-4 hover:text-primary"
      >
        Terms of Service
      </Link>{' '}
      and{' '}
      <Link
        to="/privacy"
        className="underline underline-offset-4 hover:text-primary"
      >
        Privacy Policy
      </Link>
      .
    </p>
  );
};

const signupFormProps = {
  defaultValues: {
    email: '',
  },

  resolver: yupResolver(signupSchema),
};

const SignupPage: FC = () => {
  const { isPending } = useLogin(); // Change this
  const form = useForm<SignupFormData>({ ...signupFormProps });

  const onSubmit = () => {
    console.log('Signup form submitted');
  };

  return (
    <AuthLayout contentOnLeft={false} imageSrc="Login-Image.jpg">
      <AuthForm
        form={form}
        heading={{
          title: 'Sign up',
          subtitle: 'Enter your email to create an account',
        }}
        submitText="Sign up"
        onSubmit={onSubmit}
        isPending={isPending}
      >
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Email</FormLabel>
              <FormControl>
                <FormInput placeholder="Enter your email" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
      </AuthForm>
      <TermsPrompt />
      <RedirectPrompt
        to="/login"
        className="mt-20"
        prompt="Already have an account?"
        redirect="Log in"
      />
    </AuthLayout>
  );
};

export default SignupPage;
