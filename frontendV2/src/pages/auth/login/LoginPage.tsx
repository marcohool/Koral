import { FC, useState } from 'react';
import AuthLayout from 'pages/auth/AuthLayout';
import { useForm } from 'react-hook-form';
import Input from 'components/input';
import Label from 'components/label';
import Checkbox from 'components/checkbox';
import Button from 'components/button';
import { FcGoogle } from 'react-icons/fc';
import { Link } from 'react-router-dom';

const LoginPage: FC = () => {
  const { register, handleSubmit } = useForm();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const onSubmit = () => {
    console.log('Form Submitted');
  };

  return (
    <AuthLayout contentOnLeft={true} imageSrc="Login-Image.jpg">
      <div className="flex flex-col space-y-3 text-center mb-6">
        <h1 className="text-4xl font-normal tracking-tight">Login</h1>
        <p className="text-sm text-muted-foreground">
          Enter your email & password to login to your account
        </p>
      </div>
      <form
        onSubmit={void handleSubmit(onSubmit)}
        className="mx-auto flex flex-col justify-center space-y-5 px-10 w-[450px] md:w-[500px] lg:px-16"
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
        </div>
        <div className="flex justify-between">
          <div className="flex items-center space-x-2">
            <Checkbox id="remember" />
            <Label htmlFor="remember">Remember me</Label>
          </div>
          <Label>
            <Link to="/help">Forgot password?</Link>
          </Label>
        </div>
        <Button type="submit">Log In</Button>
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
            <FcGoogle className="mr-2 h-4 w-4 animate-spin" />
          ) : (
            <FcGoogle className="mr-2 h-4 w-4" />
          )}
          Google
        </Button>
      </form>
    </AuthLayout>
  );
};

export default LoginPage;
