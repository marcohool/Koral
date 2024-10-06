import {FC} from 'react';
import Button from 'components/button';

const Components: FC = () => {
  return (
    <div className="px-4 py-4 flex justify-center items-center flex-col gap-10">
      <h1 className="text-5xl">Components</h1>
      <div className="flex flex-col gap-4">
      <Button size="lg" variant="default" onClick={() => console.log('test')}>Click Here</Button>
      <Button size="lg" variant="secondary" onClick={() => console.log('test')}>Click Here</Button>
      <Button size="lg" variant="outline" onClick={() => console.log('test')}>Click Here</Button>
      </div>
    </div>
  );
};

export default Components