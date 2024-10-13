import { FieldValues, UseFormReturn } from 'react-hook-form';
import Form from 'components/form';
import { ReactNode } from 'react';
import Button from 'components/button';
import Spinner from 'components/spinner';
import { FcGoogle } from 'react-icons/fc';

interface AuthFormProps<T extends FieldValues> {
  onSubmit: () => void;
  form: UseFormReturn<T>;
  heading: { title: string; subtitle: string };
  children: ReactNode;
  submitText: string;
  isPending: boolean;
}

const AuthForm = <T extends FieldValues>({
  form,
  heading,
  children,
  submitText,
  isPending,
  onSubmit,
}: AuthFormProps<T>) => {
  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="mx-auto flex flex-col justify-center space-y-6 w-full"
      >
        <div className="flex flex-col space-y-3 text-center mb-6">
          <h1 className="text-4xl font-normal tracking-tight">
            {heading.title}
          </h1>
          <p className="text-sm text-muted-foreground">{heading.subtitle}</p>
        </div>
        <div className="my-4 space-y-5">{children}</div>

        <div className="flex flex-col space-y-4 w-full pt-4">
          <Button type="submit" disabled={isPending}>
            {isPending && <Spinner className="mr-2 h-4 w-4 animate-spin" />}
            {submitText}
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
          <Button variant="outline" type="button" disabled={isPending}>
            {isPending ? (
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

export default AuthForm;
