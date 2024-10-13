import { FieldValues, UseFormReturn } from 'react-hook-form';
import Form from 'components/form';
import { ReactNode } from 'react';
import Button from 'components/button';
import Spinner from 'components/spinner';
import { FcGoogle } from 'react-icons/fc';
import { FormMessage } from 'components/form/Form';

interface AuthFormProps<T extends FieldValues> {
  onSubmit: (data: T) => void;
  form: UseFormReturn<T>;
  heading: { title: string; subtitle: string };
  children: ReactNode;
  submitText: string;
  isPending: boolean;
}

const FormHeading = ({
  title,
  subtitle,
  className,
}: {
  title: string;
  subtitle: string;
  className?: string;
}) => (
  <div className={className}>
    <h1 className="text-4xl font-normal tracking-tight">{title}</h1>
    <p className="text-sm text-muted-foreground">{subtitle}</p>
  </div>
);

const FormError = ({ error }: { error: string }) => (
  <FormMessage
    className="mx-auto text-center"
    style={{ whiteSpace: 'pre-line' }}
  >
    {error}
  </FormMessage>
);

const FormContent = ({
  children,
  error,
  className,
}: {
  children: ReactNode;
  error?: string;
  className?: string;
}) => (
  <div className={className}>
    {error && <FormError error={error} />}
    {children}
  </div>
);

const FormButtons = ({
  isPending,
  submitText,
  className,
}: {
  isPending: boolean;
  submitText: string;
  className?: string;
}) => (
  <div className={className}>
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
);

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
        className="mx-auto flex flex-col justify-center space-y-6 w-full "
      >
        <FormHeading
          title={heading.title}
          subtitle={heading.subtitle}
          className={'flex flex-col space-y-3 text-center'}
        />
        <FormContent
          error={form.formState.errors.root?.message}
          className="my-4 space-y-5"
        >
          {children}
        </FormContent>
        <FormButtons
          isPending={isPending}
          submitText={submitText}
          className="flex flex-col space-y-4 w-full pt-4"
        />
      </form>
    </Form>
  );
};

export default AuthForm;
