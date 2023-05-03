// @ts-check
// Note: type annotations allow type checking and IDEs autocompletion

const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'Sao Viet Portal',
  tagline:
    'A portal for Sao Viet company to manage their business and employees',
  favicon: 'img/favicon.png',

  // Set the production url of your site here
  url: 'https://foxminchan.github.io',
  // Set the /<baseUrl>/ pathname under which your site is served
  // For GitHub pages deployment, it is often '/<projectName>/'
  baseUrl: '/SaoVietPortal',

  // GitHub pages deployment config.
  // If you aren't using GitHub pages, you don't need these.
  organizationName: 'foxminchan', // Usually your GitHub org/user name.
  projectName: 'SaoVietPortal', // Usually your repo name.

  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',

  // Even if you don't use internalization, you can use this field to set useful
  // metadata like html lang. For example, if your site is Chinese, you may want
  // to replace "en" with "zh-Hans".
  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      /** @type {import('@docusaurus/preset-classic').Options} */
      ({
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),
          // Please change this to your repo.
          // Remove this to remove the "edit this page" links.
          editUrl: 'https://github.com/foxminchan/SaoVietPortal',
        },
        blog: {
          showReadingTime: true,
          // Please change this to your repo.
          // Remove this to remove the "edit this page" links.
          editUrl: 'https://github.com/foxminchan/SaoVietPortal',
        },
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
      }),
    ],
  ],

  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      // Replace with your project's social card
      image: 'img/social-card.jpg',
      navbar: {
        title: 'Developer',
        logo: {
          alt: 'Sao Viet Portal Logo',
          src: 'img/logo.jpg',
        },
        items: [
          {
            type: 'docSidebar',
            sidebarId: 'tutorialSidebar',
            position: 'left',
            label: 'Docs',
          },
          { to: 'docs/guides/guides', label: 'Guides', position: 'left' },
          {
            href: 'https://blogdaytinhoc.com/',
            label: 'Website',
            position: 'left',
            target: '_self'
          },
          {
            href: '/',
            label: 'Home',
            position: 'right',
          },
          {
            href: 'https://github.com/foxminchan/SaoVietPortal',
            label: 'GitHub',
            position: 'right',
          },
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: 'Legal & Privacy',
            items: [
              {
                label: 'Terms of Use',
                to: 'https://blogdaytinhoc.com/chinh-sach-chung-va-dieu-khoan-trung-tam-tin-hoc-sao-viet-222',
              },
              {
                label: 'Privacy Policy',
                to: 'https://blogdaytinhoc.com/chinh-sach-bao-mat-thong-tin-trung-tam-tin-hoc-sao-viet-220',
              },
              {
                label: 'Complaints',
                to: 'https://blogdaytinhoc.com/quy-trinh-giai-quyet-khieu-nai-trung-tam-tin-hoc-sao-viet-221',
              }
            ],
          },
          {
            title: 'Contact',
            items: [
              {
                label: 'Facebook',
                href: 'https://www.facebook.com/FoxMinChan/',
              },
              {
                label: 'Github',
                href: 'https://github.com/foxminchan',
              },
              {
                label: 'Developer email',
                href: 'mailto:nguyenxuannhan407@gmail.com',
              },
              {
                label: 'Guide email',
                href: 'mailto:nd.anh@hutech.edu.vn',
              },
            ],
          },
          {
            title: 'More',
            items: [
              {
                label: 'Contact us',
                to: 'mailto:trungtamtinhocsaoviet@gmail.com',
              },
              {
                label: 'Fanpage',
                to: 'https://www.facebook.com/trungtamtinhocvanphongsaoviet/',
              },
              {
                label: 'Company',
                href: 'https://blogdaytinhoc.com/',
              },
            ],
          },
        ],

        copyright: `
        <p>
          Made with 💖 by open source contributors using
          <a href="https://docusaurus.io" target="_blank" rel="noreferrer noopener">
            <span aria-label="Docusaurus">🦖</span>
          </a>
        </p>
        <p>
          Copyright © ${new Date().getFullYear()} 
          <a href="https://blogdaytinhoc.com/" target="_blank">Sao Viet Portal</a>, Inc.
        </p>`,

      },
      prism: {
        theme: lightCodeTheme,
        darkTheme: darkCodeTheme,
      },
    }),
};

module.exports = config;
