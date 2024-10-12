import { FC } from 'react';
import Button from 'components/button';
import ThemeEditor from 'shadcn-theme-editor';
import Input from 'components/input';
import { Link } from 'react-router-dom';

const ComponentsPage: FC = () => {
  return (
    <>
      <div className="px-4 py-4 flex justify-center items-center flex-col gap-10">
        <h1 className="text-5xl">Components</h1>
        <div className="flex flex-col gap-4">
          <Button
            size="lg"
            variant="default"
            onClick={() => console.log('test')}
          >
            Click Here
          </Button>
          <Button
            size="lg"
            variant="secondary"
            onClick={() => console.log('test')}
          >
            Click Here
          </Button>
          <Button
            size="lg"
            variant="outline"
            onClick={() => console.log('test')}
          >
            Click Here
          </Button>
          <Input />
          <>
            <p className="px-8 text-center text-sm text-muted-foreground">
              By clicking continue, you agree to our{' '}
              <Link
                to="/terms"
                className="underline underline-offset-4 hover:text-primary"
              >
                Terms of Service
              </Link>{' '}
              and{' '}
              <Link
                to="/privacy"
                className="underline underline-offset-4 hover:text-primary"
              >
                Privacy Policy
              </Link>
              .
            </p>
          </>
        </div>
      </div>
      <ThemeEditor />
    </>
  );
};

export default ComponentsPage;
