import { FC } from 'react';
import AuthLayout from 'pages/auth/AuthLayout';
import Label from 'components/label';
import { Link } from 'react-router-dom';
import LoginForm from 'pages/auth/login/LoginForm';

const LoginPage: FC = () => {
  return (
    <AuthLayout contentOnLeft={true} imageSrc="Login-Image.jpg">
      <div className="px-4 w-max-[450px] md:w-[500px] lg:px-16">
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
