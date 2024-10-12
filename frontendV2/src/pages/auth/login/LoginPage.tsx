import { FC } from 'react';
import AuthLayout from 'pages/auth/AuthLayout';

const LoginPage: FC = () => {
  return (
    <AuthLayout formOnLeft={true} imageSrc="Login-Image.jpg">
      <div>Login Page</div>
    </AuthLayout>
  );
};

export default LoginPage;
