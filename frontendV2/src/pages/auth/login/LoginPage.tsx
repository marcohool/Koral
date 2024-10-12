import React, { FC, useState } from 'react';
import AuthLayout from 'pages/auth/AuthLayout';
import { useForm } from 'react-hook-form';
import Input from 'components/input';
import Label from 'components/label';
import Checkbox from 'components/checkbox';
import Button from 'components/button';
import { FcGoogle } from 'react-icons/fc';
import { Link } from 'react-router-dom';
import { cn } from 'utils/utils';
import { buttonVariants } from 'components/button/Button';
import { GoArrowLeft } from 'react-icons/go';
import Spinner from 'components/spinner';

const LoginPage: FC = () => {
  const { register, handleSubmit } = useForm();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const onSubmit = (event: React.SyntheticEvent) => {
    setIsLoading(true);

    setTimeout(() => {
      event.preventDefault();

      setIsLoading(false);
    }, 3000);
  };

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
        <form
          onSubmit={onSubmit}
          className="mx-auto flex flex-col justify-center space-y-6 w-full"
        >
          <div className="my-4 space-y-5">
            <Input
              type="email"
              placeholder="Email"
              {...register('email', { required: true })}
            />
            <Input
              type="password"
              placeholder="Password"
              {...register('password', { required: true })}
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
          </div>

          <div className="flex flex-col space-y-4 w-full">
            <Button type="submit" disabled={isLoading}>
              {isLoading && <Spinner className="mr-2 h-4 w-4 animate-spin" />}
              Log In
            </Button>
            <div className="relative">
              <div className="absolute inset-0 flex items-center">
                <span className="w-full border-t" />
              </div>
              <div className="relative flex justify-center text-xs uppercase">
                <span className="bg-background px-2 text-muted-foreground">
                  Or continue with
                </span>
              </div>
            </div>
            <Button variant="outline" type="button" disabled={isLoading}>
              {isLoading ? (
                <Spinner className="mr-2 h-4 w-4 animate-spin" />
              ) : (
                <FcGoogle className="mr-2 h-4 w-4" />
              )}
              Google
            </Button>
          </div>
        </form>
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
