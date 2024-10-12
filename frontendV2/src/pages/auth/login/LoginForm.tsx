import { FC, useState } from 'react';
import { useForm } from 'react-hook-form';
import loginSchema, { LoginFormData } from 'pages/auth/login/loginSchema';
import { yupResolver } from '@hookform/resolvers/yup';
import Form from 'components/form';
import {
  FormControl,
  FormField,
  FormInput,
  FormItem,
  FormLabel,
  FormMessage,
} from 'components/form/Form';
import Button from 'components/button';
import Spinner from 'components/spinner';
import { FcGoogle } from 'react-icons/fc';
import Checkbox from 'components/checkbox';
import Label from 'components/label';
import { Link } from 'react-router-dom';

const LoginForm: FC = () => {
  const form = useForm<LoginFormData>({
    defaultValues: {
      email: '',
      password: '',
    },
    // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
    resolver: yupResolver(loginSchema),
  });
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const onSubmit = (data: LoginFormData) => {
    setIsLoading(true);

    console.log(data);
  };

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="mx-auto flex flex-col justify-center space-y-6 w-full"
      >
        <div className="my-4 space-y-5">
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
    </Form>
  );
};

export default LoginForm;
