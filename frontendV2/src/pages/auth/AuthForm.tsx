import { FC, ReactNode } from 'react';
import { UseFormReturn } from 'react-hook-form';

interface AuthFormProps {
  onSubmit: () => void;
  form: UseFormReturn;
  isPending: boolean;
  submitButtonText: string;
  errorMessage?: string;
  children: ReactNode;
  showRememberMe?: boolean;
  showForgotPassword?: boolean;
  showSocialLogin?: boolean;
}

const AuthForm: FC<AuthFormProps> = () => {
  return <div>AuthForm</div>;
};

export default AuthForm;
