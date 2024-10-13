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
    </AuthLayout>
  );
};

export default SignupPage;
