import { FC } from 'react';
import Label from 'components/label';
import { Link } from 'react-router-dom';

const RedirectPrompt: FC<{
  to: string;
  className: string;
  prompt: string;
  redirect: string;
}> = ({ to, className, prompt, redirect }) => {
  return (
    <Link to={to} className={className}>
      <Label>
        {prompt}{' '}
        <span className="font-semibold hover:cursor-pointer">{redirect}</span>
      </Label>
    </Link>
  );
};

export default RedirectPrompt;
