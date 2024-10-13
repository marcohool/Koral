import { FC } from 'react';
import AuthLayout from 'pages/auth/AuthLayout';
import Label from 'components/label';
import { Link, useNavigate } from 'react-router-dom';
import useLogin from 'pages/auth/login/useLogin';
import { Control, useForm } from 'react-hook-form';
import loginSchema, { LoginFormData } from 'pages/auth/login/loginSchema';
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
import Checkbox from 'components/checkbox';
import RedirectPrompt from 'pages/auth/RedirectPrompt';

const FormContent: FC<{ control: Control<LoginFormData> }> = ({ control }) => {
  return (
    <>
      <FormField
        control={control}
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
      <FormField
        control={control}
        name="password"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Password</FormLabel>
            <FormControl>
              <FormInput
                placeholder="Enter your password"
                {...field}
                type={'password'}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <div className="flex justify-between">
        <div className="flex items-center space-x-2">
          <Checkbox id="remember" />
          <Label htmlFor="remember">Remember me</Label>
        </div>
        <Label>
          <Link to="/help">Forgot password?</Link>
        </Label>
      </div>
    </>
  );
};

const loginFormProps = {
  defaultValues: {
    email: '',
    password: '',
  },

  resolver: yupResolver(loginSchema),
};

const LoginPage: FC = () => {
  const { mutate: login, isPending } = useLogin();
  const form = useForm<LoginFormData>({ ...loginFormProps });
  const navigate = useNavigate();

  const onSubmit = (data: LoginFormData) => {
    login(data, {
      onSuccess: () => {
        navigate('/');
      },
      onError: (error) => {
        form.setError('root', {
          type: 'manual',
          message:
            (error.response?.data as string) ??
            `An unexpected error has occurred. Please try again later\n${error.message}`,
        });
      },
    });
  };

  return (
    <AuthLayout contentOnLeft={true} imageSrc="Login-Image.jpg">
      <AuthForm
        form={form}
        heading={{
          title: 'Log in',
          subtitle: 'Enter your email & password to login to your account',
        }}
        submitText="Log in"
        onSubmit={onSubmit}
        isPending={isPending}
      >
        <FormContent control={form.control} />
      </AuthForm>
      <RedirectPrompt
        to={'/signup'}
        className="mt-20"
        prompt="Don't have an account?"
        redirect="Sign up"
      />
    </AuthLayout>
  );
};

export default LoginPage;
