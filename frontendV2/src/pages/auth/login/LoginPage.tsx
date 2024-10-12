import { FC } from 'react';
import AuthLayout from 'pages/auth/AuthLayout';
import Label from 'components/label';
import { Link } from 'react-router-dom';
import { cn } from 'utils/utils';
import { buttonVariants } from 'components/button/Button';
import { GoArrowLeft } from 'react-icons/go';
import LoginForm from 'pages/auth/login/LoginForm';

const LoginPage: FC = () => {
  return (
    <AuthLayout contentOnLeft={true} imageSrc="Login-Image.jpg">
      <Link
        to="/"
        className={cn(
          buttonVariants({ variant: 'ghost' }),
          'absolute left-2 top-2 sm:top-4 sm:left-4 md:left-6 md:top-6 lg:left-8 lg:top-8',
        )}
      >
        <GoArrowLeft className="size-5" />
        <span className="ml-2 hidden sm:inline">Home</span>
      </Link>

      <div className="px-4 w-max-[450px] md:w-[500px] lg:px-16">
        <div className="flex flex-col space-y-3 text-center mb-6">
          <h1 className="text-4xl font-normal tracking-tight">Login</h1>
          <p className="text-sm text-muted-foreground">
            Enter your email & password to login to your account
          </p>
        </div>
        <LoginForm />
      </div>
      <Link to="/signup" className="pt-20">
        <Label>
          Don't have an account?{' '}
          <span className="font-semibold hover:cursor-pointer">Sign up</span>
        </Label>
      </Link>
    </AuthLayout>
  );
};

export default LoginPage;
