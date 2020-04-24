export interface MenuItem {
  link: string;
  icon: string;
  alt: string;
}

export const menuItems: MenuItem[] = [
  {
    link: '/',
    icon: 'home',
    alt: 'Go to the front page'
  },
  {
    link: '/simulation',
    icon: 'control_point',
    alt: 'Create a new plant configuration'
  }
];
