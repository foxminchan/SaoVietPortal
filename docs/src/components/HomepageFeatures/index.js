import React from 'react';
import clsx from 'clsx';
import styles from './styles.module.css';

const FeatureList = [
  {
    title: 'Easy to Manage',
    Svg: require('@site/static/img/management.svg').default,
    description: (
      <>
          With Sao Viet Portal, you can easily manage your website with a few clicks.
      </>
    ),
  },
  {
    title: 'Intelligent with Chatbot',
    Svg: require('@site/static/img/chatbot.svg').default,
    description: (
      <>
          Chatbot is a new technology that is being used by many businesses.
      </>
    ),
  },
  {
    title: 'Powered by Microsoft',
    Svg: require('@site/static/img/microsoft.svg').default,
    description: (
      <>
         With the power of Microsoft, Sao Viet Portal is a great choice for your business.
      </>
    ),
  },
];

function Feature({Svg, title, description}) {
  return (
    <div className={clsx('col col--4')}>
      <div className="text--center">
        <Svg className={styles.featureSvg} role="img" />
      </div>
      <div className="text--center padding-horiz--md">
        <h3>{title}</h3>
        <p>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures() {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}
