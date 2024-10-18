import { FC } from 'react';
import Sheet, { SheetTrigger } from 'components/sheet';
import Button from 'components/button';
import MenuIcon from 'components/navbar/icons/MenuIcon';
import { SheetContent } from 'components/sheet/Sheet';
import { Link } from 'react-router-dom';
import { getPageData } from 'App/pagesData';
import { Page } from 'App/router.types';
import { cn } from 'utils/utils';

const PageData: Page[] = [Page.Home];

const LandingNavbar: FC<{
  scrolled: boolean;
  pages?: Page[];
  className?: string;
}> = ({ scrolled, pages = PageData, className }) => {
  return (
    <header
      className={`flex sticky top-0 z-50 font-butler justify-center font-medium lg:px-14 px-8 transition-all items-center duration-500 ease-out ${scrolled ? 'h-32 text-primary-foreground' : 'bg-background h-16'}`}
    >
      <div
        className={cn('flex justify-between w-full items-center', className)}
      >
        <Link
          to="#"
          className={`transition-all duration-500 ease-out font-medium ${scrolled ? 'text-6xl mb-2' : 'text-2xl'}`}
        >
          Koral
        </Link>
        <nav className="hidden lg:flex gap-24 transition-all duration-500 ease-out">
          {pages.map((page) => {
            const pageData = getPageData(page);

            if (!pageData) throw new Error('Page not found');

            return (
              <Link to={pageData.path} className="mr-6" key={pageData.path}>
                {pageData.title}
              </Link>
            );
          })}
        </nav>
      </div>
      <Sheet modal={false}>
        <SheetTrigger asChild>
          <Button
            variant="outline"
            size="icon"
            className="lg:hidden border-0 shadow-none bg-transparent"
          >
            <MenuIcon className="h-6 w-6" />
          </Button>
        </SheetTrigger>
        <SheetContent side="right">
          {pages.map((page) => {
            const pageData = getPageData(page);

            if (!pageData) throw new Error('Page not found');

            return (
              <Link
                to={pageData.path}
                className="flex w-full items-center py-2 text-lg font-semibold"
                key={pageData.path}
              >
                {pageData.title}
              </Link>
            );
          })}
        </SheetContent>
      </Sheet>
    </header>
  );
};

export default LandingNavbar;
