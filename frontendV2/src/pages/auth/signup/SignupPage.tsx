import { FC, useState } from 'react';
import AuthLayout from '../AuthLayout';
import AuthForm from 'pages/auth/AuthForm';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import {
  SignupEmailFormData,
  signupSchema,
} from 'pages/auth/signup/signupSchema';
import {
  FormControl,
  FormField,
  FormInput,
  FormItem,
  FormLabel,
  FormMessage,
} from 'components/form/Form';
import RedirectPrompt from 'pages/auth/RedirectPrompt';
import { Link, useNavigate } from 'react-router-dom';
import SignupPasswordForm from 'pages/auth/signup/SignupPasswordForm';
import useAuth from '@/context/useAuth';

export const TermsPrompt = () => {
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
  const form = useForm<SignupEmailFormData>({ ...signupFormProps });

  const navigate = useNavigate();

  const [passwordForm, setPasswordForm] = useState(false);
  const [email, setEmail] = useState('');

  const { setToken } = useAuth();
  setToken(null);

  const onSubmit = (data: SignupEmailFormData) => {
    setEmail(data.email);
    setPasswordForm(true);
  };

  return (
    <AuthLayout
      contentOnLeft={false}
      imageSrc="Signup-Image.jpg"
      redirect={
        passwordForm
          ? {
              text: 'Back',
              onClick: () => {
                setPasswordForm(false);
              },
            }
          : {
              text: 'Home',
              onClick: () => {
                navigate('/');
              },
            }
      }
    >
      {passwordForm ? (
        <SignupPasswordForm email={email} />
      ) : (
        <>
          <AuthForm
            form={form}
            heading={{
              title: 'Sign up',
              subtitle: 'Enter your email to create an account',
            }}
            submitText="Sign up"
            onSubmit={onSubmit}
            isPending={false}
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
        </>
      )}
    </AuthLayout>
  );
};

export default SignupPage;
